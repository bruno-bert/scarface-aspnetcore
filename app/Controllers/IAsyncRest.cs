using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Resources;

namespace <%= data.schema.appconfig.name %>.Controllers {
    interface IAsyncRest<TResource> where TResource : IResource {
        Task<IActionResult> GetAll ();
        Task<IActionResult> Get (QueryParameters _params);
        Task<IActionResult> GetId (int? id);
        Task<IActionResult> Create (TResource item);
        Task<IActionResult> Edit (int? id, TResource item);
        Task<IActionResult> Delete (int id);

    }
}