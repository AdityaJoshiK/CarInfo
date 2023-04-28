using CarInfo.BAL;
using CarInfo.DAL;
using CarInfo.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace CarInfo.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration Configuration;
        public LoginController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyBqg2CIrBzaV8p-S63a-XrfFHbqEaKdR6A"));
        }

        FirebaseAuthProvider auth;

        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("_UserToken");

            if (token != null)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(LoginModel loginModel)
        {
            try
            {
                string connstr = this.Configuration.GetConnectionString("myConnectionString");
                SEC_DAL dal = new SEC_DAL();
               dal.dbo_PR_MST_Client_Insert(connstr, loginModel.Email, loginModel.Password); ;

                //create the user
                await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                //log in the new user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();

        }

        //[HttpPost]
        //public async Task<IActionResult> SignIn(LoginModel loginModel)
        //{
        //    try
        //    {
        //        //log in an existing user
        //        var fbAuthLink = await auth
        //                        .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
        //        string token = fbAuthLink.FirebaseToken;
        //        //save the token to a session variable
        //        if (token != null)
        //        {
        //            ClientCV.SetUserEmail(loginModel.Email);
        //            HttpContext.Session.SetString("_UserToken", token);

        //            return RedirectToAction("Index");
        //        }

        //    }
        //    catch (FirebaseAuthException ex)
        //    {
        //        var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
        //        ModelState.AddModelError(String.Empty, firebaseEx.error.message);
        //        return View(loginModel);
        //    }

        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {
            try
            {
                //log in an existing user
                string connstr = this.Configuration.GetConnectionString("myConnectionString");
                SEC_DAL dal = new SEC_DAL();
                //dal.dbo_PR_MST_Client_Insert(connstr, loginModel.Email, loginModel.Password);
                DataTable dt;

                    // Email login
                    dt = dal.PR_MST_Client_SelectByUserNamePassword(connstr, loginModel.Email, loginModel.Password);

                    if (dt.Rows.Count > 0)
                    {
                        // Successful login, store user information in session variables
                        foreach (DataRow dr in dt.Rows)
                        {
                            HttpContext.Session.SetString("Email", dr["Email"].ToString());
                            HttpContext.Session.SetString("ClientID", dr["ClientID"].ToString());
                            HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            //HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                            //HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                            //HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                            //break;
                        }

                        
                    }

                    var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //save the token to a session variable
                if (token != null)
                {
                    ClientCV.SetUserEmail(loginModel.Email);
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }
    }
}
