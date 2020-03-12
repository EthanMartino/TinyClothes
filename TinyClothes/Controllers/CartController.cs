using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class CartController : Controller
    {
        private readonly StoreContext _context;
        //Reads cookie data
        private readonly IHttpContextAccessor _http;

        public CartController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        // Display all products in cart
        public IActionResult Index()
        {
            return View();
        }

        // Add a single item to the shopping cart
        public async Task<IActionResult> Add(int id, string prevUrl)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            if (c != null)
            {
                CartHelper.Add(c, _http);
            }

            return Redirect(prevUrl);
        }

        public void AddJS(int id)
        {
            //TODO: Get id of clothing
            //TODO: Add Clothing to cart
            // TODO: Send success response
        }

        // Summary/checkout page
        public IActionResult Checkout()
        {
            return View();
        }
    }
}