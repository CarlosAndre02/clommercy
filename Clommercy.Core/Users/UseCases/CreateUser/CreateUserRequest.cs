using MediatR;

namespace Clommercy.Core.Users.UseCases.CreateUser
{
    public sealed record CreateUserRequest(
        string Email, string Name, string Password) :
         IRequest<CreateUserResponse>;

}