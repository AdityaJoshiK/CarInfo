using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace CarInfo.BAL
{
    public class ClientCheckAccess : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                var sessionToken = controller.HttpContext.Session.GetString("_UserToken");
                if (string.IsNullOrEmpty(sessionToken))
                {
                    // User is not logged in, redirect to login page
                    filterContext.Result = new RedirectResult("~/Login/SignIn");
                }
                else
                {
                    // User is logged in, continue with the request
                    base.OnActionExecuting(filterContext);
                }
            }
            else
            {
                throw new Exception("Controller not found in filter context");
            }
        }
    //        public async void OnAuthorization(AuthorizationFilterContext filterContext)
    //{
    //    var rd = filterContext.RouteData;
    //    string currentAction = rd.Values["action"].ToString();
    //    string currentController = rd.Values["controller"].ToString();
    //    //string currentArea = rd.DataTokens["area"].ToString();

    //    if (filterContext.HttpContext.Session.GetString("email") == null)
    //    {
    //        filterContext.Result = new RedirectResult("~/Login/SignIn");
    //    }
    //    else
    //    {
    //        var email = filterContext.HttpContext.User.FindFirst("email").Value;
    //        var auth = FirebaseAuth.DefaultInstance;
    //        var user = await auth.GetUserByEmailAsync(email);
    //        filterContext.HttpContext.Session.SetString("email", user.Email);
    //    }
    //}

    //public override void OnResultExecuting(ResultExecutingContext filterContext)
    //{
    //    filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    //    filterContext.HttpContext.Response.Headers["Expires"] = "-1";
    //    filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
    //    base.OnResultExecuting(filterContext);
    //}
    }

}