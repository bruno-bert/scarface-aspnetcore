using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using <%= data.schema.appconfig.name %>.Models;

namespace <%= data.schema.appconfig.name %>.Infrastructure {
    public interface IAsyncRepository<TEntity> where TEntity : IEntity {

       
        Task<int> CountAsync ();

        Task<IEnumerable<dynamic>> GetAsync (QueryParameters _params);

        Task<IEnumerable<TEntity>> GetAllAsync ();

        Task<TEntity> GetIdAsync (int? id);

        Task<TEntity> CreateAsync (TEntity item);

        Task<TEntity> EditAsync (int? id, TEntity item);

        Task DeleteAsync (int id);

    }
}