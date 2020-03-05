using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyClothes.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Results = new List<Clothing>();
        }

        public string Size { get; set; }

        /// <summary>
        /// The type of clothing (Shirt, Pants, etc)
        /// </summary>
        public string Type { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [Display(Name = "Minimum Price")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Minimum value must be a positive number.")]
        public double? MinPrice { get; set; }

        [Display(Name = "Maximum Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Maximum value must be a positive number.")]
        public double? MaxPrice { get; set; }

        public List<Clothing> Results { get; set; }
    }
}
