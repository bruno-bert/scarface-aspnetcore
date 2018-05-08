using System;

namespace <%= data.schema.appconfig.name %>.Models {

    public interface IEntity {

        int Id { get; set; }
        DateTime ChangedAt { get; set; }
        string ChangedBy { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
    }

}