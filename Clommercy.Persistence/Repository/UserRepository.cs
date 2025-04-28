using Clommercy.Core.Users.Domain;
using Clommercy.Core.Users.Repository;
using Clommercy.Persistence.AdoNet;

using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly IAdoNetContext _context;
    public UserRepository(IAdoNetContext context)
    {
        _context = context;
    }

    public User Get(int id)
    {
        using (SqlCommand command = _context.CreateQueryCommand("SELECT * FROM Users WHERE Id = @Id"))
        {
            command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                User? user = null;
                while (reader.Read())
                {
                    user = Map(reader);
                }
                return user is not null ? user : throw new Exception("User not found");
            }
        }
    }

    public Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public int Create(User user)
    {
        using (SqlCommand command = _context.CreateCommand("INSERT INTO Users (Email, Name, Password) VALUES(@Email, @Name, @Password); SELECT SCOPE_IDENTITY()"))
        {
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Password", user.Password);
            int userId = Convert.ToInt32(command.ExecuteScalar());
            return userId;
        }
    }

    public void Update(User user)
    {
        using (SqlCommand command = _context.CreateCommand("UPDATE Users SET Email = @Email, Name = @Name WHERE Id = @userId;"))
        {
            command.Parameters.AddWithValue("@userId", user.Id);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int Id)
    {
        using (var command = _context.CreateCommand("DELETE FROM Users WHERE Id = @userId"))
        {
            command.Parameters.AddWithValue("userId", Id);
            command.ExecuteNonQuery();
        }
    }

    public Task<User> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> FindUsers(string Name)
    {
        // using (var command = _context.CreateCommand("dasdsa"))
        // {
        //     command.CommandText = @"SELECT * FROM Users WHERE CompanyId = @companyId AND FirstName LIKE @firstName";
        //     command.Parameters.AddWithValue("@Name", Name);
        //     return ToList(command);
        // }
        throw new NotImplementedException();
    }
}
