using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.AdoNet;

public interface IAdoNetContext
{
    SqlTransaction CreateTransaction();
    void DisposeTransaction();
    SqlCommand CreateCommand(string sql);
    void Dispose();

}
