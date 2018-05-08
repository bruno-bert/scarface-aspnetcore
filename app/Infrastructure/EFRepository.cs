using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using <%= data.schema.appconfig.name %>.Models;


namespace <%= data.schema.appconfig.name %>.Infrastructure {
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : GenericEntity {

        protected readonly AppConfig appConfig;
        protected readonly DbContext _context;

        protected DbSet<TEntity> dbSet;

        public EFRepository (IOptions<AppConfig> appConfigAccessor, AppDBContext context) {
            this._context = context;
            this.dbSet = _context.Set<TEntity> ();
            this.appConfig = appConfigAccessor.Value;
        }

      
        public virtual int Count () {
            return dbSet.Count ();
        }

        public virtual IEnumerable<TEntity> GetAll () {
            return dbSet.ToList ();
        }

        public virtual IEnumerable<dynamic> Get (QueryParameters _params) {

            string[] related = null;
            string [] values = null;
            string delim = appConfig.queryStringDelimiter;

            IQueryable<dynamic> builder = dbSet.AsQueryable ();

            if (_params == null)
                return builder.ToList ();

            if (_params.related != null) {
                related = _params.related.Split(delim).Select(item => item.Trim()).ToArray();
                builder = related.Aggregate (builder, (query, path) => query.Include (path.Trim ()));
            }

            if ((_params.filter != null) && (_params.filterValues != null)) {
                values = _params.filterValues.Split(delim).Select(item => item.Trim()).ToArray();              
                builder = builder.Where (_params.filter, values);
            }

            if (_params.order != null)
                builder =  builder.OrderBy (_params.order);
    


            if (_params.group != null)
              builder = (IQueryable<dynamic>) builder.GroupBy("new(" + _params.group + ")"); 

            
            if (_params.fields != null) 
                builder = (IQueryable<dynamic>)  builder.Select ("new(" + _params.fields + ")");   
            
          
            if ( (_params.pagingSettings !=null) && (_params.pagingSettings.pagesize.HasValue && _params.pagingSettings.page.HasValue) )
                builder =  builder.Page ((int) _params.pagingSettings.page, (int) _params.pagingSettings.pagesize);

            return builder.ToList();

        }

        public virtual TEntity GetId (int? id) {
            return dbSet.SingleOrDefault (m => m.Id == id);
        }

        public TEntity Edit (int? id, TEntity item) {

            if ((item == null) || (id != item.Id))
                return null;

            if (!ItemExists (id))
                return null;        

            //TODO - improve way to set default values
            item.ChangedAt = DateTime.Today;
            item.CreatedAt = DateTime.Today;
            item.ChangedBy = "User";
            item.CreatedBy = "User";
            
            _context.Entry(item).Property(c => c.CreatedBy).IsModified = false;
            _context.Entry(item).Property(c => c.CreatedAt).IsModified = false;

            _context.Update (item);
            _context.SaveChanges ();

            return item;
        }

        public void Delete (int id) {

            var item = dbSet.SingleOrDefault (m => m.Id == id);
            if (item != null) {
                dbSet.Remove (item);
                _context.SaveChanges ();
            }

            return;

        }

        public TEntity Create (TEntity item) {

             //TODO - improve way to set default values
            item.ChangedAt = DateTime.Today;
            item.CreatedAt = DateTime.Today;
            item.ChangedBy = "User";
            item.CreatedBy = "User";
           
            _context.Add (item);
            _context.SaveChanges ();

            return item;
        }

        private bool ItemExists (int? id) {
            if (id != null)
                return dbSet.Any (e => e.Id == id);
            else
                throw new Exception ("Item cannot be null");
        }
 

      

    }
}