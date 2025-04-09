namespace Clommercy.Core.Users.UseCases.CreateUser;

public sealed record CreateUserResponse
{
    public int Id { get; private set; }
    
    public CreateUserResponse(int id)
    {
        Id = id;
    }
}
