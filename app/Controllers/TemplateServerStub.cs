   /// <summary>
   /// <%= path.summary %>
   /// </summary>
   /// <remarks><%= data.path.description %></remarks>   

    <%_ if (data.path.parameters) { _%>  

   <%_ data.path.parameters.forEach(function(parameter) { _%>   
   /// <param name="<%= parameter.name %>"><%= parameter.description %></param>
   <%_ }); _%>

   <%_ } _%> 

   <%_ data.path.responses.forEach(function(response) { _%>
   /// <response code="<%= response.code %>"><%= response.description %></response>
   <%_ }); _%> 
   [<%= data.path.verb %>]
   [Route("<%= data.path.route %>")]
   [SwaggerOperation("<%= data.path.operationId %>")]
   <%_ data.path.responses.forEach(function(response) { _%>
   [ProducesResponseType(typeof(<%= data.path.schema %>Resource), <%= response.code %>)]
   <%_ }); _%>
   public <% if (data.path.override) { %> override <% } %> virtual IActionResult <%= data.path.operationId %>(
            <%_ let counter = 1 ; _%>

        <%_ if (data.path.parameters) { _%>    

   			<%_ data.path.parameters.forEach(function(parameter) { _%>  
   			[<%= parameter.in %>] <%= parameter.type %> <%= parameter.name %>
   			<%_ if (counter != data.path.parameters.length) { _%>, <%_ } _%> <%_ counter+=1; _%>
   			<%_ }); _%>

         <%_ } _%>
   			)
   { 
   	   <%_ if(data.path.base){ _%>
		   base.<%= data.path.base %>(
		            <%_ let counter = 1 ; _%>

              <%_ if (data.path.parameters) { _%>  

		   	    	<%_ data.path.parameters.forEach(function(parameter) { _%>  
		   			<%= parameter.name %> 
		   			<%_ if (counter != data.path.parameters.length) { _%>, <%_ } _%> <%_ counter+=1; _%>
		   			<%_ }); _%>	

             <%_ } _%>

		   );
		 <%_ } else{ _%>  
		    throw new NotImplementedException();
	   <%_ } _%> 	
      
   }