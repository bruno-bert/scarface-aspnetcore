using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace <%= data.schema.appconfig.name %>.Infrastructure.Filters {

    public class CustomValidateModelAttribute : ActionFilterAttribute {

        public override void OnActionExecuting (ActionExecutingContext context) {

            if (!context.ModelState.IsValid) {
                context.Result = new BadRequestObjectResult (context.ModelState);
            }
        }
        
    }

}