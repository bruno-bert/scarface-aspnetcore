using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace <%= data.schema.appconfig.name %>.Infrastructure {

    public class ConditionalRequireHttpsAttribute : RequireHttpsAttribute
    {

        private readonly AppConfig appConfig;

        public ConditionalRequireHttpsAttribute(IOptions<AppConfig> appConfigAcessor)
        {
            this.appConfig = appConfigAcessor.Value;
        }
        
        public override void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            

            if (!appConfig.securityOptions.useSsl)
              return;
            

            base.OnAuthorization(filterContext);
        }
    }
}