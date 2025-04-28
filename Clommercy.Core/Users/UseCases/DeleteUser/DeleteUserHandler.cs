using AutoMapper;

using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Repository;

using MediatR;

namespace Clommercy.Core.Users.UseCases.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.Get(request.Id);
        if (user is null) return default;

        _unitOfWork.StartTransaction();
        _userRepository.Delete(request.Id);

        _unitOfWork.Commit();

        return Task.FromResult(_mapper.Map<DeleteUserResponse>(request.Id));
    }
}