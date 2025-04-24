using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Clommercy.Persistence.AdoNet;

public class AdoNetContext : IAdoNetContext
{
    public readonly SqlConnection _connection;
    public SqlTransaction? _transaction { get; private set; }

    public AdoNetContext(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();

        RunInitCommands();
    }

    public SqlTransaction CreateTransaction()
    {
        var transaction = _connection.BeginTransaction();
        _transaction = transaction;

        return transaction;
    }

    public void DisposeTransaction()
    {
        if (_transaction == null) return;
        _transaction.Dispose();
    }

    public SqlCommand CreateCommand(string sql)
    {
        if (_transaction == null) throw new ArgumentNullException("Cannot create a command without a transaction.");
        return new SqlCommand(sql, _connection, _transaction);
    }

    public SqlCommand CreateQueryCommand(string sql)
    {
        return new SqlCommand(sql, _connection);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    private void RunInitCommands() {
        new SqlCommand(ReadSqlScript("CREATE_USER_TABLE.sql"), _connection).ExecuteNonQuery();
    }

    private string ReadSqlScript(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        // For the resource to be valid, it needs to be declared as EmbeddedResource in Clommercy.Persistence.csproj
        string fullResourceName = $"Clommercy.Persistence.SqlServer.{resourceName}";
        
        using (Stream? stream = assembly.GetManifestResourceStream(fullResourceName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException($"The embedded resource '{fullResourceName}' was not found.");
            }
            
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
