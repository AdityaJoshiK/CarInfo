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
    public class ClientCheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
    public async void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        var rd = filterContext.RouteData;
        string currentAction = rd.Values["action"].ToString();
        string currentController = rd.Values["controller"].ToString();
        //string currentArea = rd.DataTokens["area"].ToString();

        if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        {
            filterContext.Result = new RedirectResult("~/Login/SignIn");
        }
        else
        {
            var email = filterContext.HttpContext.User.FindFirst("email").Value;
            var auth = FirebaseAuth.DefaultInstance;
            var user = await auth.GetUserByEmailAsync(email);
            filterContext.HttpContext.Session.SetString("UserID", user.Uid);
        }
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        filterContext.HttpContext.Response.Headers["Expires"] = "-1";
        filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
        base.OnResultExecuting(filterContext);
    }
    }

}