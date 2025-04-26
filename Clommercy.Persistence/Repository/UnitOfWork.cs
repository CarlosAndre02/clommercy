using Clommercy.Core.Shared.Contracts;
using Clommercy.Persistence.AdoNet;

using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly IAdoNetContext _context;
    private SqlTransaction? _transaction;

    public UnitOfWork(IAdoNetContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.DisposeTransaction();
        _context.Dispose();
    }

    public void StartTransaction() {
        if(_transaction is not null) throw new InvalidOperationException("There's already a transaction valid");
        _transaction = _context.CreateTransaction();
    }

    public void Commit()
    {
        if(_transaction is null) throw new ArgumentNullException("Cannot commit a null transaction");
        try
        {
            _transaction.Commit();
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _context.DisposeTransaction();
        }
    }
}
