using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public static class SessionHelper
    {
        private const string IdKey = "Id";
        private const string UsernameKey = "Username";

        public static void CreateUserSession(int id, string username, IHttpContextAccessor http)
        {
            http.HttpContext.Session.SetInt32(key: IdKey, value: id);
            http.HttpContext.Session.SetString(key :UsernameKey, value:  username);
        }

        /// <summary>
        /// Logs user out by clearing out the session.
        /// </summary>
        /// <param name="http"></param>
        public static void DestroyUserSession(IHttpContextAccessor http)
        {
            http.HttpContext.Session.Clear();
        }

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static bool IsUserLoggedIn(IHttpContextAccessor http)
        {
            int? memberId = http.HttpContext.Session.GetInt32(IdKey);
            if (memberId.HasValue)
            {
                return true;
            }
            return false;
        }
    }
}
