using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    /// <summary>
    /// Represents a single clothing item.
    /// </summary>
    public class Clothing
    {
        /// <summary>
        /// The unique identifier for the clothing item
        /// </summary>
        [Key] // Sets it as Primary Key
        public int ItemId { get; set; }

        /// <summary>
        /// Size of the clothing (Small, Medium, Large)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing (shirt, pants, etc...)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The color of the clothing item
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Retail Price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The display Title of the clothing item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the clothing item
        /// </summary>
        public string Description { get; set; }
    }
}
