using AutoMapper;
using MediatR;

using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Domain;
using Clommercy.Core.Users.Repository;

namespace Clommercy.Core.Users.UseCases.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        _unitOfWork.StartTransaction();
        int userId = _userRepository.Create(user);

        _unitOfWork.Commit();

        return Task.FromResult(_mapper.Map<CreateUserResponse>(userId));
    }
}
