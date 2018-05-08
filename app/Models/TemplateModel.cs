using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace <%= data.schema.appconfig.name %>.Models {

    [Table("<%= data.model.additionalProperties.tableName %>")]
    public class <%= data.model.name %> : GenericEntity {
        <% data.model.properties.forEach(function(property) { %>
        <% if(property.annotations) property.annotations.forEach(function(annotation) { %>
        <%= annotation %><% }); -%> 
        [Column("<%= property.name %>")]
        public <%- property.type %> <%= property.name %> { get; set; }  <% }); -%>


       public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("class <%= data.model.name %> {\n");
            <% data.model.properties.forEach(function(property) { %>
            sb.Append("  <%= property.name %>: ").Append(<%= property.name %>).Append("\n");    <% }); -%>

            sb.Append("}\n");

            return sb.ToString();
        }



        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((<%= data.model.name %>)obj);
        }



         public bool Equals(<%= data.model.name %> other)
        {

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
           <% let counter = 1 ; %>
           <% data.model.properties.forEach(function(property) { %>
           ( this.<%= property.name %> == other.<%= property.name %> || 
              this.<%= property.name %> != null &&  this.<%= property.name %>.Equals(other.<%= property.name %>))  <% if (counter != data.model.properties.length) {%> && <% } %> <% counter+=1; %>  <% }); -%> ;
        }



         public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 41;
                <% data.model.properties.forEach(function(property) { %>
                if (this.<%= property.name %> != null)
                hash = hash * 59 + this.<%= property.name %>.GetHashCode();  <% }); -%>                    
                return hash;
            }
        }





    }
}