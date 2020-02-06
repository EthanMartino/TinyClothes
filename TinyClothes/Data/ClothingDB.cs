using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyClothes.Models;

namespace TinyClothes.Data
{
    /// <summary>
    /// Contains Database helper methods for <see cref="Models.Clothing"/>
    /// </summary>
    public static class ClothingDB
    {
        /// <summary>
        /// Returns total number of clothing items
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetNumClothing(StoreContext context)
        {
            // LINQ Method Syntax
            return await context.Clothing.CountAsync();

            // LINQ Query Syntax
            //return await (from c in context.Clothing
            //              select c).CountAsync();
        }

        /// <summary>
        /// Returns a specific page of clothing items sorted by ItemId in ascending order.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<Clothing>> GetClothingByPage(StoreContext context, int pageNum, int pageSize)
        {
            // To avoid skipping rows for page 1
            const int PageOffset = 1;

            // LINQ Method Syntax
            List<Clothing> clothes =
                await context.Clothing
                .OrderBy(c => c.ItemId)
                .Skip((pageNum - PageOffset) * pageSize) // Must do Skip then Take
                .Take(pageSize)
                .ToListAsync();

            return clothes;

            //// LINQ Query Syntax
            //List<Clothing> clothes2 =
            //    await (from c in context.Clothing
            //           orderby c.ItemId ascending
            //    select c).ToListAsync();
        }

        /// <summary>
        /// Returns a single clothing item or null if there is no match
        /// </summary>
        /// <param name="id">The ID of the clothing item</param>
        /// <param name="context">DB context</param>
        /// <returns></returns>
        public static async Task<Clothing> GetClothingById(int id, StoreContext context)
        {
            Clothing c =
                await (from clothing in context.Clothing
                 where clothing.ItemId == id
                 select clothing).SingleOrDefaultAsync();

            return c;
        }

        /// <summary>
        /// Adds a clothing object to the database. Returns the object with the Id populated.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static async Task<Clothing> Add(Clothing c, StoreContext context)
        {
            await context.AddAsync(c); // Prepares INSERT query
            await context.SaveChangesAsync(); // Executes INSERT query

            return c;
        }
    }
}
