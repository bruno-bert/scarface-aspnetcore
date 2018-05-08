using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using <%= data.schema.appconfig.name %>.Infrastructure;

namespace <%= data.schema.appconfig.name %>.Infrastructure 
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile ($"appsettings.Development.json", optional : true)
                .Build();
     
            var builder = new DbContextOptionsBuilder<AppDBContext>();

            var cs = Configuration.GetConnectionString ("DBConnectionString"); 
        
            //builder.UseSqlServer(cs);
	    builder.UseNpgsql(cs);
            
            return new AppDBContext(builder.Options);
        }

    }
}