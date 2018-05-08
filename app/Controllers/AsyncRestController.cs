using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Infrastructure.Filters;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Resources;

namespace <%= data.schema.appconfig.name %>.Controllers {
    
    [CustomValidateModel]
    [ServiceFilter(typeof(ConditionalRequireHttpsAttribute))]
    public abstract class AsyncRestController<TEntity, TResource> : Controller, IAsyncRest<TResource>
        where TEntity : GenericEntity
    where TResource : GenericResource {

       
        protected readonly IAsyncRepository<TEntity> _repository;
        protected readonly AppConfig appConfig;

        public AsyncRestController (IOptions<AppConfig> appConfigAcessor, IAsyncRepository<TEntity> repository) {
            this._repository = repository;
            this.appConfig = appConfigAcessor.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get ([FromQuery] QueryParameters _params) {
            return Ok (Json (await _repository.GetAsync (_params)));
        }

        [HttpGet ("all")]
        public async Task<IActionResult> GetAll () {
            return Ok (Json (await _repository.GetAllAsync ()));
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetId (int? id) {

            TEntity item = null;

            item = await _repository.GetIdAsync (id);

            if (item == null)
                return NotFound ();

            return Ok (Mapper.Map<TResource> (item));
        }

        [HttpGet ("paging")]
        public async Task<IActionResult> GetPaging ([FromQuery] PagingSettings _pagingSettings) {

            Boolean hasPaging = false;
            int page = 1;
            int pageSize = appConfig.pageSize;

            if (_pagingSettings != null) {
                hasPaging = true;
                page = _pagingSettings.page ?? page;
                pageSize = _pagingSettings.pagesize ?? pageSize;
            }

            var totalItems = await _repository.CountAsync ();

            if (!hasPaging)
                pageSize = totalItems;

            int currentPage = page;
            int currentPageSize = pageSize;

            var totalPages = (int) Math.Ceiling ((double) totalItems / pageSize);

            IEnumerable<dynamic> _items = await _repository.GetAllAsync ();

            JsonResult json = Json (_items.Skip ((currentPage - 1) * currentPageSize)
                .Take (currentPageSize).ToList ());

            Response.AddPagination (page, pageSize, totalItems, totalPages);

            return Ok (json);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id) {
            await _repository.DeleteAsync (id);
            return NoContent ();

        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] TResource item) {
            TEntity entity = await _repository.CreateAsync (Mapper.Map<TEntity> (item));
            return Ok (Mapper.Map<TResource> (entity));
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Edit (int? id, [FromBody] TResource item) {

            if ((item == null) || (id != item.Id))
                return NotFound ();
          
            TEntity entity = await _repository.EditAsync (id, Mapper.Map<TEntity> (item));

            return Ok (Mapper.Map<TResource> (entity));
        }

    }
}