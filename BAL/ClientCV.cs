using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace CarInfo.BAL
{
    public static class ClientCV
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static ClientCV()
        {
            // You need to register IHttpContextAccessor in your DI container to use it
            //_httpContextAccessor = (IHttpContextAccessor)Startup.StaticServiceProvider.GetService(typeof(IHttpContextAccessor));
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static string? UserEmail()
        {
            string? userEmail = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("Email") != null)
            {
                userEmail = _httpContextAccessor.HttpContext.Session.GetString("Email").ToString();
            }

            return userEmail;
        }

        public static void SetUserEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                _httpContextAccessor.HttpContext.Session.SetString("Email", email);
            }
            else
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
            }
        }
    }
}
