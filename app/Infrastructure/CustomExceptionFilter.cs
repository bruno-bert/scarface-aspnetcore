using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace <%= data.schema.appconfig.name %>.Infrastructure.Filters {
    
    #region snippet_ExceptionFilter
    public class CustomExceptionFilter : IExceptionFilter {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;


        public CustomExceptionFilter (
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider) {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public  void OnException (ExceptionContext context) {

            var error = new ApiError ();

            error.Message = context.Exception.Message;

            if (context.Exception.InnerException!=null)		
              error.Detail = context.Exception.InnerException.Message;
       

        context.Result = new BadRequestObjectResult (error) {
            StatusCode = 500
        };

    }
}
#endregion
}