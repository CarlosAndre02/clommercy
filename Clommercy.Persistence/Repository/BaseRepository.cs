using System.Diagnostics.CodeAnalysis;

using Clommercy.Core.Shared.Contracts;
using Clommercy.Persistence.AdoNet;

using Microsoft.Data.SqlClient;

namespace Clommercy.Persistence.Repository;

public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
{
    public BaseRepository()
    {
    }

    protected IEnumerable<TEntity> ToList(SqlCommand command)
    {
        using (var record = command.ExecuteReader())
        {
            List<TEntity> items = new List<TEntity>();
            while (record.Read())
            {
                #pragma warning disable CS8604
                items.Add(Map(record)); 
            }
            return items;
        }
    }

    protected TEntity? Map(SqlDataReader record)
    {
        if (record == null || !record.HasRows)
            return default;

        var objT = Activator.CreateInstance<TEntity>();
        foreach (var property in typeof(TEntity).GetProperties())
        {
            try
            {
                int columnOrdinal;
                try
                {
                    columnOrdinal = record.GetOrdinal(property.Name);
                }
                catch (IndexOutOfRangeException)
                {
                    // Column not found in result set, skip this property
                    continue;
                }

                if (!record.IsDBNull(columnOrdinal))
                {
                    object value = record.GetValue(columnOrdinal);

                    if (value != null)
                    {
                        // Convert the database value to the property's type
                        object convertedValue = Convert.ChangeType(value, property.PropertyType);
                        property.SetValue(objT, convertedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mapping property {property.Name}: {ex.Message}");
                continue; // Skip problematic properties
            }
        }
        return objT;
    }
}
