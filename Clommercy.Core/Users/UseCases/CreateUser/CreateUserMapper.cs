using AutoMapper;

using Clommercy.Core.Users.Domain;

namespace Clommercy.Core.Users.UseCases.CreateUser;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, CreateUserResponse>();
    }
}
