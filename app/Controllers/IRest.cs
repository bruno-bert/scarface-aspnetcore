using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Resources;


namespace <%= data.schema.appconfig.name %>.Controllers {
    interface IRest<TResource> where TResource : IResource {
        
        IActionResult GetAll ();
        IActionResult Get (QueryParameters _params);
        IActionResult GetId (int? id);
        IActionResult Create (TResource item);
        IActionResult Edit (int? id, TResource item);
        IActionResult Delete (int id);

    }
}