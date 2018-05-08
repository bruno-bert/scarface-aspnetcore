using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using <%= data.schema.appconfig.name %>.Models;

namespace <%= data.schema.appconfig.name %>.Resources {

     public class <%= data.model.name %>Resource : GenericResource {

        <% data.model.properties.forEach(function(property) { %>
        <% if(property.annotations) property.annotations.forEach(function(annotation) { %>
        <%= annotation %><% }); -%> 
        /// <summary>
        /// <%= property.description %>
        /// </summary>
        [Column("<%= property.name %>")]
        public <%- property.type %> <%= property.name %> { get; set; }  <% }); -%>


    }
}