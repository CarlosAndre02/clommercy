namespace Clommercy.Core.Shared.Contracts;

public interface IUnitOfWork
{
    void Dispose();
    void Commit();
}
