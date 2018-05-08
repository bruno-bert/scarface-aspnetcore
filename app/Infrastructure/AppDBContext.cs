using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using <%= data.schema.appconfig.name %>.Models;

namespace <%= data.schema.appconfig.name %>.Infrastructure
{
    public partial class AppDBContext : DbContext
    {

              <%_ Object.keys(data.schema.appmodel.definitions).forEach(function(model) { _%>
             public DbSet<<%= model %>> <%= model %> { get; set; }
             <%_ });  _%>    
             
             
        
        
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options){}

  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


       
 
    }
}