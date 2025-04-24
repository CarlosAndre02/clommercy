using MediatR;

namespace Clommercy.Core.Users.UseCases.GetUser;

public sealed record GetUserRequest(int Id) : IRequest<GetUserResponse>;
