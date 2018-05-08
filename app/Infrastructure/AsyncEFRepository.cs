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
    
    public class AsyncEFRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : GenericEntity {

        protected readonly AppConfig appConfig;
        protected readonly DbContext _context;

        protected DbSet<TEntity> dbSet;

        public AsyncEFRepository (IOptions<AppConfig> appConfigAccessor, AppDBContext context) {
            this._context = context;
            this.dbSet = _context.Set<TEntity> ();
            this.appConfig = appConfigAccessor.Value;
        }

        
        public async Task<int> CountAsync () {
            return await dbSet.CountAsync ();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync () {
            return await dbSet.ToListAsync ();
        }

        public virtual async Task<IEnumerable<dynamic>> GetAsync (QueryParameters _params) {
           

            string[] related = null;
            string [] values = null;
            string delim = appConfig.queryStringDelimiter;

            IQueryable<dynamic> builder = dbSet.AsQueryable ();

            if (_params == null)
                return await builder.ToListAsync ();

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
            
            if (_params.pagingSettings!=null)
             if (_params.pagingSettings.pagesize.HasValue && _params.pagingSettings.page.HasValue)
                 builder =  builder.Page ((int) _params.pagingSettings.page, (int) _params.pagingSettings.pagesize);

            return await builder.ToListAsync();

        
        }

        public virtual async Task<TEntity> GetIdAsync (int? id) {
            return await dbSet.SingleOrDefaultAsync (m => m.Id == id);
        }

        public async Task<TEntity> EditAsync (int? id, TEntity item) {

            if ((item == null) || (id != item.Id))
                return null;

            if (await ItemExistsAsync (id) == false)
                return null;

             //TODO - improve way to set default values
            item.ChangedAt = DateTime.Today;
            item.CreatedAt = DateTime.Today;
            item.ChangedBy = "User";
            item.CreatedBy = "User";
            
            _context.Entry(item).Property(c => c.CreatedBy).IsModified = false;
            _context.Entry(item).Property(c => c.CreatedAt).IsModified = false;

            _context.Update (item);
            await _context.SaveChangesAsync ();

            return item;
        }

        public async Task DeleteAsync (int id) {
            var item = await dbSet.SingleOrDefaultAsync (m => m.Id == id);
            if (item != null) {
                dbSet.Remove (item);
                await _context.SaveChangesAsync ();
            }
        }

        public async Task<TEntity> CreateAsync (TEntity item) {
            
             //TODO - improve way to set default values
            item.ChangedAt = DateTime.Today;
            item.CreatedAt = DateTime.Today;
            item.ChangedBy = "User";
            item.CreatedBy = "User";

            _context.Add (item);

            await _context.SaveChangesAsync ();

            return item;
        }

        private async Task<bool> ItemExistsAsync (int? id) {

            if (id != null)
                return await dbSet.AnyAsync (e => e.Id == id);
            else
                throw new Exception ("Item cannot be null");

        }
       

    }
}