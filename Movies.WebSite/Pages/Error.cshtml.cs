using System.ComponentModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.WebSite.Pages
{
    public class ErrorModel : PageModel
    {
        public bool RouteWhereExceptionOccurredExists => !string.IsNullOrEmpty(RouteWhereExceptionOccurred);
        [DisplayName("Route where exception occurred.")]
        public string RouteWhereExceptionOccurred { get; private set; }

        public bool ExceptionMessageExists => !string.IsNullOrEmpty(ExceptionMessage);
        [DisplayName("Exception message.")]
        public string ExceptionMessage { get; private set; }

        public void OnGet()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                RouteWhereExceptionOccurred = exceptionFeature.Path;
                ExceptionMessage = exceptionFeature.Error.Message;
            }
        }
    }
}
