using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FEWebsite.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        /// <summary>
        /// Quick method to calculate an object's age.
        /// </summary>
        /// <param name="theDateTime">date object being extended</param>
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }

        /// <summary>
        /// Quick method to get the AppSettings token.
        /// </summary>
        /// <param name="config">the config</param>
        public static string GetAppSettingsToken(this IConfiguration config)
        {
            return config.GetSection("AppSettings:Token").Value;
        }
    }
}
