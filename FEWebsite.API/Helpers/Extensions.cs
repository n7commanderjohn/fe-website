using System;
using System.ComponentModel;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public static void AddPagination<T>(this HttpResponse response, PagedList<T> pagedList)
        {
            AddPagination(response,
                pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);
        }

        public static void AddPagination(this HttpResponse response,
            int currentPage, int pageSize, int totalCount, int totalPages)
        {
            var pHeader = new PaginationHeader(currentPage, pageSize, totalCount, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(pHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
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

        public static int GetUserIdFromClaim(this ControllerBase controllerBase)
        {
            return int.Parse(controllerBase.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static UnauthorizedObjectResult CheckIfLoggedInUserIsAuthorized(this ControllerBase controllerBase, int userId,
            string unauthorizedMsg = "You are not authorized for this action.")
        {
            if (userId != controllerBase.GetUserIdFromClaim())
            {
                return controllerBase.Unauthorized(new StatusCodeResultReturnObject(
                    controllerBase.Unauthorized(), unauthorizedMsg));
            }
            else
            {
                return null;
            }
        }

        public static string ToDescriptionString(this Enum enumVal)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[]) enumVal
                .GetType()
                .GetField(enumVal.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
