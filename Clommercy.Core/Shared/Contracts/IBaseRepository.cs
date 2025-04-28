namespace Clommercy.Core.Shared.Contracts;

public interface IBaseRepository<T> where T : BaseEntity
{
    int Create(T entity);
    void Update(T entity);
    void Delete(int id);
    T Get(int id);
    Task<List<T>> GetAll();
}
