using MediatR;

namespace Clommercy.Core.Users.UseCases.DeleteUser;

public sealed record DeleteUserRequest(int Id) : IRequest<DeleteUserResponse>;