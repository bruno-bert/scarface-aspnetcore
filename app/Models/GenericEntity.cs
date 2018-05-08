using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace <%= data.schema.appconfig.name %>.Models {
    public class GenericEntity : IEntity {

        [Required]
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required] 
        [Column("updatedAt")]
        public DateTime ChangedAt { get; set; }

        [Required] 
        [Column("updatedBy")]
        public string ChangedBy { get; set; }

        [Required] 
        [Column("createdBy")]
        public string CreatedBy { get; set; }
        
        [Required] 
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}