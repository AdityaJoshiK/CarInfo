using CarInfo.BAL;
using CarInfo.DAL;
using CarInfo.Email;
using CarInfo.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Reflection;

namespace CarInfo.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration Configuration;

        public readonly IEmailSender _emailSender;
        public LoginController(IConfiguration _configuration, IEmailSender emailSender)
        {
            Configuration = _configuration;
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyBqg2CIrBzaV8p-S63a-XrfFHbqEaKdR6A"));

            this._emailSender = emailSender;
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
                string otp = GenerateOTP();

                string subject = "CarInfo - Email Confirmation";
                string body = $@"
    <!DOCTYPE html>
    <html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <title>CarInfo - Email Confirmation</title>
    </head>
    <body style=""font-family: Arial, sans-serif; background-color: #f5f5f5; color: #333;"">
        <p>Thank you for registering with CarInfo!</p>
        <p>Your One-Time Password (OTP) for email confirmation is: <strong>{otp}</strong></p>
        <p>Please use this OTP to verify your email address and complete the registration process.</p>
        <p>If you didn't initiate this registration, please ignore this email.</p>
        <br>
        <p>Best Regards,<br>CarInfo Team</p>
    </body>
    </html>
";

                loginModel.EmailVerificationCode = otp;
                loginModel.IsEmailVerified = false;

                string jsonModel = JsonConvert.SerializeObject(loginModel);

                // Encode the JSON string and pass it as a query parameter
                var encodedModel = WebUtility.UrlEncode(jsonModel);

                TempData["RegistrationModel"] = encodedModel;

                await _emailSender.SendEmailAsync(loginModel.Email, subject, body);

                //return RedirectToAction("VerifyEmail", new { email = loginModel.Email });
                return RedirectToAction("VerifyEmail", new { model = encodedModel });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ModelState.AddModelError(String.Empty, "Error during registration.");
                return View(loginModel);
            }

            return View();

        }

        public static string GenerateOTP()
        {
            // Set the seed for the random number generator based on current time
            Random random = new Random((int)DateTime.Now.Ticks);

            // Generate a 6-digit OTP
            int otp = random.Next(100000, 999999);

            return otp.ToString();
        }


        [HttpGet]
        public IActionResult VerifyEmail(string email)
        {
            // Pass the email to the view
            var model = new LoginModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmailAsync(string model, string otp)
        {
            try
            {
                var decodedModel = WebUtility.UrlDecode(model);

                // Deserialize the JSON string back to a LoginModel object
                var loginModel = JsonConvert.DeserializeObject<LoginModel>(decodedModel);
                // Check if entered OTP matches the stored OTP
                if (otp == loginModel.EmailVerificationCode)
                {
                    // Mark email as verified
                    loginModel.IsEmailVerified = true;

                    // Save changes to the database or Firebase
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

                        //return RedirectToAction("Index");
                    }
                    // Redirect to login or home page
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Invalid OTP. Please try again.");
                    return View(loginModel);
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View();
            }
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
