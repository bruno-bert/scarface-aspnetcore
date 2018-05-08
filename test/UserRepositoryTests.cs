using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using <%= data.schema.appconfig.name %>.Models;
using <%= data.schema.appconfig.name %>.Infrastructure;


namespace <%= data.schema.appconfig.name %>.Tests {
      
        public class UserRepositoryTests : RepositoryTests<User> {
           
            public UserRepositoryTests (DatabaseFixture fixture) : base(fixture) {
            }                     

        }

}