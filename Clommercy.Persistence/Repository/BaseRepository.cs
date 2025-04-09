using Clommercy.Core.Shared.Contracts;
using Clommercy.Persistence.AdoNet;

using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.Repository;

public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
    private IAdoNetContext _context;
    public BaseRepository(IAdoNetContext context)
    {
        _context = context;
    }

    protected IEnumerable<TEntity> ToList(SqlCommand command)
    {
        using (var record = command.ExecuteReader())
        {
            List<TEntity> items = new List<TEntity>();
            while (record.Read())
            {       
                items.Add(Map(record));
            }
            return items;
        }
    }
        
    protected TEntity Map(SqlDataReader record)
    {
        var objT = Activator.CreateInstance<TEntity>();
        foreach (var property in typeof(TEntity).GetProperties())
        {
            var recordColumn = record[property.Name];
            if ((recordColumn is not null) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                property.SetValue(objT, recordColumn);
        }
        return objT;
    }
}
