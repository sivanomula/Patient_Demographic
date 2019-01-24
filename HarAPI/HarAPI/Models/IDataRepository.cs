using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarAPI.Models
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        int Add(TEntity entity);
        void Update(TEntity model, TEntity entity);
        void Delete(TEntity model);
    }
}
