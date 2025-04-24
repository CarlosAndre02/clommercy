using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.AdoNet;

public interface IAdoNetContext
{
    SqlTransaction CreateTransaction();
    void DisposeTransaction();
    SqlCommand CreateCommand(string sql);
    SqlCommand CreateQueryCommand(string sql);
    void Dispose();

}
