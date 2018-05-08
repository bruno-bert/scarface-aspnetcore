using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using <%= data.schema.appconfig.name %>.Models;


namespace <%= data.schema.appconfig.name %>.Infrastructure {
    public interface IRepository<TEntity> where TEntity : IEntity {

        int Count ();       
    
        IEnumerable<dynamic> Get (QueryParameters _params);

        IEnumerable<TEntity> GetAll ();

        TEntity GetId (int? id);

        TEntity Create (TEntity item);

        TEntity Edit (int? id, TEntity item);

        void Delete (int id);

       
    }
}