using AutoMapper;

using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Domain;
using Clommercy.Core.Users.Repository;

using MediatR;

namespace Clommercy.Core.Users.UseCases.GetUser;

public class GetUserHandler: IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserHandler(IUserRepository userRepository, IMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken) {
        User user = _userRepository.Get(request.Id);
        return Task.FromResult(_mapper.Map<GetUserResponse>(user));
    }
}
