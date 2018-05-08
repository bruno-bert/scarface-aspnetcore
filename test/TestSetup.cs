using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Infrastructure;



namespace <%= data.schema.appconfig.name %>.Tests {

    public abstract class DatabaseFixture<TEntity> : IDisposable {

        IRepository<TEntity> repository;

        DbContext context;


        public IRepository<TEntity> GetRepository(){
            return this.repository;
        }


        public DatabaseFixture () {
            context = CreateContext();
            repository =  GetInMemoryRepository(context);
        }

        public void Dispose () {
            context.Dispose();
        }


         private DbContext CreateContext(){
            DbContextOptions<AppDBContext> options;
            var builder = new DbContextOptionsBuilder<AppDBContext> ();
            builder.UseInMemoryDatabase ("test");
            //string cs = "Server=(localdb)\\MSSQLLocalDB;Database=test;trusted_connection=true;";
            //builder.UseSqlServer(cs);
            options = builder.Options;
            AppDBContext context = new AppDBContext (options);
            return context;
        }

        private IRepository<TEntity> GetInMemoryRepository (DbContext context) {
            context.Database.EnsureDeleted ();
            context.Database.EnsureCreated ();
           //TODO - return repository with sending the app settings
            //return new EFRepository<TEntity> (IOptions<AppConfig> appConfig, (AppDBContext)context);
            //return new EFRepository<TEntity> ((AppDBContext)context);
            return null;
        }


        protected abstract AddTestData (AppDBContext context) {
            

            context.user.AddRange(
                                    new User { Id = 1, Name = "Luke", Username = "teste", 
                                           Addresses = new List<Address>(3), 
                                           UserDetails = new UserDetails { Id = 1, UserId = 1, 
                                           UserDetails2 = new UserDetails2 { Id = 1, UserDetailsId = 1  }} },
                                    new User { Id = 2, Name = "Luke", Username = "teste" },
                                    new User { Id = 3, Name = "Luke", Username = "teste" },
                                    new User { Id = 4, Name = "Luke", Username = "teste" },
                                    new User { Id = 5, Name = "Luke", Username = "teste" });

          
                                   
            context.SaveChanges ();
        } 
       
    }


     

     

}