using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Infrastructure;

namespace <%= data.schema.appconfig.name %>.Tests {

    public class UserDatabaseFixture :  DatabaseFixture<User> {

       
        public UserDatabaseFixture () : base(){ }
      
        private override AddTestData (AppDBContext context) {
            
            //TODO - create logic to read from excel file using AutoFixture library    
            context.user.AddRange(  new User { Id = 2, Name = "Luke", Username = "usr_luke" },
                                    new User { Id = 3, Name = "Brandon", Username = "usr_brandon" },
                                    new User { Id = 4, Name = "John", Username = "usr_john" },
                                    new User { Id = 5, Name = "Lucy", Username = "usr_lucy" });
          
                                   
            context.SaveChanges ();
        } 
       
    }


     

     

}