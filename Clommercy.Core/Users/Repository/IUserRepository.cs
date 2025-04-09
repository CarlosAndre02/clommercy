using Clommercy.Core.Shared.Contracts;
using Clommercy.Core.Users.Domain;

namespace Clommercy.Core.Users.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email);
}
