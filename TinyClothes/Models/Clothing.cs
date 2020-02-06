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
        [Required(ErrorMessage = "Size is required")]
        public string Size { get; set; }

        /// <summary>
        /// The type of clothing (shirt, pants, etc...)
        /// </summary>
        [Required()]
        public string Type { get; set; }

        /// <summary>
        /// The color of the clothing item
        /// </summary>
        [Required()]
        public string Color { get; set; }

        /// <summary>
        /// Retail Price of the item
        /// </summary>
        [Range(0.0, 300.0)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        /// <summary>
        /// The display Title of the clothing item
        /// </summary>
        [Required]
        [StringLength(35)]
        //Regx can be great for validation
        //[RegularExpression("^([A-Za-z0-9])+$")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the clothing item
        /// </summary>
        [StringLength(800)]
        public string Description { get; set; }
    }
}
