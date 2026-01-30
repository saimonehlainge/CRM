using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Recruitment.Repositories;

namespace Recruitment.Helpers
{
    public class ErrorHandlerMiddleware : ActionFilterAttribute
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment hostEnvironment)
        {
            _next = next;
            _hostEnvironment = hostEnvironment;
        }

        public async Task Invoke(HttpContext context, ILoggerHelperRepository loggerHelper)
        {
            //try
            //{
                DeleteFilesAfter3Day();

            //  Handle specific HTTP status codes
            //            switch (context.Response.StatusCode)
            //{
            //	case 404:
            //		HandlePageNotFound(context);
            //		break;

            //	default:
            //		break;
            //}

            await _next(context);
            //}

            //catch (Exception e)
            //{
            //    //  Handle uncaught global exceptions (treat as 500 error)
            //    //HandleException(context, e);
            //    throw e;
            //}
        }

        //  500
        private static void HandleException(HttpContext context, Exception e)
        {
            context.Response.Redirect("/Error/Error500");
        }

        //  404
        private static void HandlePageNotFound(HttpContext context)
        {
            //  Display an information page that displays the bad url using a cookie
            string pageNotFound = context.Request.Path.ToString().TrimStart('/');
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMilliseconds(10000);
            cookieOptions.IsEssential = true;
            context.Response.Cookies.Append("PageNotFound", pageNotFound, cookieOptions);
            context.Response.Redirect("/Error/Error404");
        }

        public void DeleteFilesAfter3Day()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/");
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] datafile = directoryInfo.GetFiles("*");
                DateTime day3 = DateTime.Now.AddDays(-3);
                foreach (FileInfo file in datafile.OrderByDescending(c => c.CreationTime))
                {
                    if (file.CreationTime.Date < day3.Date)
                    {
                        if (file.Name != "XMLFile1.xml")
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("เกิดข้อผิดพลาด ในการลบไฟล์");
            }
        }
    }
}
