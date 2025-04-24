using Clommercy.Core.Shared.Contracts;

namespace Clommercy.Core.Users.Domain;

public sealed class User : BaseEntity
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }

    public User(string email, string name, string password) {
        Email = email;
        Name = name;
        Password = password;
    }

    public User() { }
}
