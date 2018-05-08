using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Infrastructure;


namespace <%= data.schema.appconfig.name %>.Tests {

      
        public abstract class RepositoryTests<TEntity> : IClassFixture<DatabaseFixture> {

            DatabaseFixture fixture;
            
            
            IRepository<TEntity> repository;

            List<TEntity> result = null ;

            public RepositoryTests (DatabaseFixture fixture) {
                this.fixture = fixture;
                this.repository = this.fixture.GetRepository ();
            }

        
            [Fact]
            public void GetAll () {
                List<TEntity> list = (List<TEntity>)repository.GetAll();
                Assert.Equal(3,list.Count);
            }


            [Fact]
            public void GetListWithSelect () {
                 try {
                    result = (List<TEntity>) repository.Get (new QueryParameters (null,  null, null, null, null, null, "Id"));
                    Assert.NotNull(result[0].Id);                    
                } catch(Exception ex){
                     Assert.True(false, ex.Message); 
                }
            }


            [Fact]
            public void GetListWithFilter () {
                try {
                    result = (List<TEntity>) repository.Get (new QueryParameters (null,  "Id == @0","1", null, null, null, null));
                    Assert.Equal(1, result.Count);
                } catch(Exception ex){
                     Assert.True(false, ex.Message); 
                }
            }


            [Fact]
            public void GetListWithFilterContains () {
                try{
                    
                    result =  (List<TEntity>) repository.Get (new QueryParameters ( null, "Name.Contains(@0)", "Joh", null, null, null, null)); 
                    Assert.Equal(1, result.Count);
                 }
                 catch(Exception ex){
                      Assert.True(false, ex.Message);  
                 }
            }

            [Fact]
            public void GetListWithSelectAndFilter () {
                   result =  (List<TEntity>) repository.Get (new QueryParameters ( null, "Id == @0 and Name.Contains(@1)", "1, John", null, null, null, "Id,Name")); 
                    Assert.Equal(1, result.Count);
                    Assert.NotNull(result[0].Id);
            }


             [Theory]
             [InlineData("UserDetails.UserDetails2")]
            public void GetListWithInclude (string value) {
               result =  (List<TEntity>) repository.Get (new QueryParameters ( null, null, null, null, null, value, null)); 
                 Assert.NotNull(result[0].UserDetails.UserDetails2);
            }


             [Theory]
             [InlineData("UserDetails.UserDetails2", null)]
             public void GetListWithIncludeAndSelect (string related, string fields) {
               result =  (List<User>) repository.Get (new QueryParameters ( null, null, null, null, null,related, fields)); 
               Assert.NotNull(result[0].UserDetails.UserDetails2 );
            }

          

            [Fact]
            public void GetListWithGroupBy () {

                  try{
                    result =  (List<TEntity>) repository.Get (new QueryParameters ( null, null, null,"Username", null, null, "Username"));
                    //Assert.Equal(1, result.Count);
                 }
                 catch(Exception ex){
                      Assert.True(false, ex.Message);  
                 }

              
            }
            

        }

    }

}