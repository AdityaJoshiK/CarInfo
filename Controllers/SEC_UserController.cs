using CarInfo.DAL;
using CarInfo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CarInfo.Controllers
{
    public class SEC_UserController : Controller
    {
        private IConfiguration Configuration;
        public SEC_UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            string connstr = this.Configuration.GetConnectionString("myConnectionString");
            string error = null;
            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Login");
            }
            else
            {
                SEC_DAL dal = new SEC_DAL();
                DataTable dt;

                if (!string.IsNullOrEmpty(modelSEC_User.Email))
                {
                    // Email login
                    dt = dal.PR_MST_Client_SelectByUserNamePassword(connstr, modelSEC_User.Email, modelSEC_User.Password);

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

                        // Redirect to client page
                        return RedirectToAction("Index","ClientHome");
                    }
                }
                else
                {
                    // Username/password login
                    dt = dal.PR_MST_User_SelectByUserNamePassword(connstr, modelSEC_User.UserName, modelSEC_User.Password);

                    if (dt.Rows.Count > 0)
                    {
                        // Successful login, store user information in session variables
                        foreach (DataRow dr in dt.Rows)
                        {
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                            HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                            HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                            break;
                        }

                        // Redirect to index page
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Login failed, redirect to login page with error message
                TempData["Error"] = "User name or password is invalid!";
                return RedirectToAction("Login");

            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
