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
        // return Task.FromResult<List<User>>(new List<User>());
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
        using (var command = _context.CreateCommand("dasdsa"))
        {
            command.CommandText = @"UPDATE Users SET CompanyId = @companyId WHERE Id = @userId";
            command.Parameters.AddWithValue("@Email", user.Email);
            command.ExecuteNonQuery();
        }
    }

    public void Delete(User user)
    {
        using (var command = _context.CreateCommand("dasdsa"))
        {
            command.CommandText = @"DELETE FROM Users WHERE Id = @userId";
            command.Parameters.AddWithValue("userId", user.Id);
            command.ExecuteNonQuery();
        }
    }

    public Task<User> GetByEmail(string email)
    {
        // var user = new User(email: "dsadasd", name: "dsadasd", password: "dsadasd");
        // return Task.FromResult<User>(user);
        throw new NotImplementedException();
    }

    public IEnumerable<User> FindUsers(string Name)
    {
        using (var command = _context.CreateCommand("dasdsa"))
        {
            command.CommandText = @"SELECT * FROM Users WHERE CompanyId = @companyId AND FirstName LIKE @firstName";
            command.Parameters.AddWithValue("@Name", Name);
            return ToList(command);
        }
    }

    public IEnumerable<User> FindBlocked()
    {
        using (var command = _context.CreateCommand("dasdsa"))
        {
            command.CommandText = @"SELECT * FROM Users WHERE Status = -1";
            return ToList(command);
        }
    }
}
