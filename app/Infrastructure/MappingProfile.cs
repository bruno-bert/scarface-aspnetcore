using System;
using AutoMapper;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Resources;

namespace <%= data.schema.appconfig.name %>.Infrastructure {
    public class MappingProfile : Profile {
        public MappingProfile () {

               <%_ Object.keys(data.schema.appmodel.definitions).forEach(function(model) { _%>
               
	           CreateMap<<%= model %>, <%= model %>Resource> ();
	           CreateMap<<%= model %>Resource, <%= model %>> ();

	           <%_ });  _%>
            
           
             
        }

    }



}