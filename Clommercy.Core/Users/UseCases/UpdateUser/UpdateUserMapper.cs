using AutoMapper;

using Clommercy.Core.Users.Domain;

namespace Clommercy.Core.Users.UseCases.UpdateUser;

public class UpdateUserMapper : Profile
{
    public UpdateUserMapper() {
        CreateMap<UpdateUserRequest, User>();
        CreateMap<User, UpdateUserResponse>();
    }

}
