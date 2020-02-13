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
    }
}
