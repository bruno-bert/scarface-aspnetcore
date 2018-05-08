using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using <%= data.schema.appconfig.name %>.Infrastructure;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Resources;

namespace <%= data.schema.appconfig.name %>.Controllers {
   

    [Route ("api/<%= data.model %>")]
    public class <%= data.model %>Controller : AsyncRestController<<%= data.model %>, <%= data.model %>Resource> {

        public <%= data.model %>Controller (IOptions<AppConfig> appConfigAcessor, 
                            IAsyncRepository<<%= data.model %>> repository) : base (appConfigAcessor, repository) { }

   <%_ data.paths.forEach(function(path) { _%>
       <%- path %>
   <%_ }); _%>                       

    }

}