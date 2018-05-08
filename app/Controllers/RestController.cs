using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Infrastructure.Filters;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Resources;

namespace <%= data.schema.appconfig.name %>.Controllers {

    [CustomValidateModel]
    [ServiceFilter(typeof(ConditionalRequireHttpsAttribute))]
    public abstract class RestController<TEntity, TResource> : Controller, IRest<TResource>
        where TEntity : GenericEntity
    where TResource : GenericResource {

        protected readonly IRepository<TEntity> _repository;

        protected readonly AppConfig appConfig;

        public RestController (IOptions<AppConfig> appConfigAcessor, IRepository<TEntity> repository) {
            this._repository = repository;
            this.appConfig = appConfigAcessor.Value;
        }

        [HttpGet ("all")]
        public virtual IActionResult GetAll () {
            return Ok (_repository.GetAll ());
        }

        [HttpGet]
        public virtual IActionResult Get ([FromQuery] QueryParameters _params) {
            return Ok (_repository.Get (_params));
        }

        [HttpGet ("paging")]
        public IActionResult GetPaging ([FromQuery] PagingSettings _pagingSettings) {

            Boolean hasPaging = false;

            int page = 1;
            int pageSize = 0;

            if (_pagingSettings != null) {
                hasPaging = true;
                page = _pagingSettings.page ?? default (int);
                pageSize = _pagingSettings.pagesize ?? default (int);
            }

            var totalItems = _repository.Count ();

            if (!hasPaging)
                pageSize = totalItems;

            int currentPage = page;
            int currentPageSize = pageSize;

            var totalPages = (int) Math.Ceiling ((double) totalItems / pageSize);

            IEnumerable<dynamic> _items = _repository.GetAll ();

            JsonResult json = Json (_items.Skip ((currentPage - 1) * currentPageSize)
                .Take (currentPageSize).ToList ());

            Response.AddPagination (page, pageSize, totalItems, totalPages);

            return json;
        }

        [HttpGet ("{id}")]
        public virtual IActionResult GetId (int? id) {

            var item = _repository.GetId (id);

            if (item == null)
                return NotFound ();

            return Ok (Mapper.Map<TResource> (item));
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            _repository.Delete (id);
            return NoContent ();

        }

        [HttpPut ("{id}")]
        public IActionResult Edit (int? id, [FromBody] TResource item) {

            if ((item == null) || (id != item.Id))
                return NotFound ();

           
            TEntity entity = _repository.Edit (id, Mapper.Map<TEntity> (item));

            return Ok (Mapper.Map<TResource> (entity));

        }

        [HttpPost]
        public IActionResult Create ([FromBody] TResource item) {

          
            TEntity entity = _repository.Create (Mapper.Map<TEntity> (item));
           
            return Ok (Mapper.Map<TResource> (entity));

        }

    }
}