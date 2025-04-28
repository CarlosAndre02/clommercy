using AutoMapper;
using MediatR;

using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Repository;


namespace Clommercy.Core.Users.UseCases.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _userRepository.Get(request.Id);
        if (user is null) return default;

        user.Email = request.Email;
        user.Name = request.Name;

        _unitOfWork.StartTransaction();
        _userRepository.Update(user);

        _unitOfWork.Commit();

        return Task.FromResult(_mapper.Map<UpdateUserResponse>(user));
    }
}
