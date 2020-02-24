using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    public static class AccountDB
    {
        internal static async Task<bool> IsUsernameTaken(string username, StoreContext context)
        {
            bool isTaken = await
                (from a in context.Accounts
                 where a.Username == username
                 select a).AnyAsync();

            return isTaken;
        }

        public static async Task<Account> Register(StoreContext context, Account acc)
        {
            await context.Accounts.AddAsync(acc);
            await context.SaveChangesAsync();
            return acc;
        }

        /// <summary>
        /// Returns the account of the user with the supplied login credentials.
        /// Null is returned if there is no match.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<Account> DoesUserMatch(LoginViewModel login, StoreContext context)
        {
            Account acc =
             await (from user in context.Accounts
             where (user.Email == login.UsernameOrEmail ||
             user.Username == login.UsernameOrEmail) &&
             user.Password == login.Password
             select user).SingleOrDefaultAsync();

            return acc;
        }
    }
}
