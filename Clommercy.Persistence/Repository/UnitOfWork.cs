using Clommercy.Core.Shared.Contracts;
using Clommercy.Persistence.AdoNet;

using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly IAdoNetContext _context;
    private SqlTransaction _transaction;

    public UnitOfWork(IAdoNetContext context)
    {
        _context = context;
        _transaction = context.CreateTransaction();
    }

    public void Dispose()
    {
        _context.DisposeTransaction();
        _context.Dispose();
    }

    public void Commit()
    {
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
            _transaction = _context.CreateTransaction();
        }
    }
}
