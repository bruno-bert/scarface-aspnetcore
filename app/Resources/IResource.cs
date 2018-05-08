using System;

namespace <%= data.schema.appconfig.name %>.Resources {

   
    public interface IResource {

        int Id { get; set; }
        DateTime ChangedDate { get; set; }
        string ChangedBy { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
      

    }
    
}