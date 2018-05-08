using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace <%= data.schema.appconfig.name %>.Resources {
    
    public class GenericResource : IResource {

        public int Id { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        
    }
}