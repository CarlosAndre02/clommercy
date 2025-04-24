using AutoMapper;

using Clommercy.Core.Users.Domain;

namespace Clommercy.Core.Users.UseCases.GetUser;

public class GetUserMapper : Profile
{
    public GetUserMapper() {
        CreateMap<User, GetUserResponse>();
    }

}
