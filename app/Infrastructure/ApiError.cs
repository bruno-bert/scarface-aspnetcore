using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using <%= data.schema.appconfig.name %>.Models;

namespace <%= data.schema.appconfig.name %>.Infrastructure {
    public class ApiError {

        public String Message { get; set; }
        public String Detail { get; set; }

       

    }
}