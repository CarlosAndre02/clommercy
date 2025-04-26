using MediatR;

namespace Clommercy.Core.Users.UseCases.UpdateUser;

public sealed record UpdateUserRequest(int Id, string Email, string Name) : IRequest<UpdateUserResponse>;
