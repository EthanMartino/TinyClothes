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

        public async Task<JsonResult> AddJS(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);

            if (c == null)
            {
                //Return not found message (404)
            }

            CartHelper.Add(c, _http);

            // TODO: Send success response
            JsonResult result = new JsonResult("Success");
            result.StatusCode = 200; // Http "Ok" status code
            return result;
        }

        // Summary/checkout page
        public IActionResult Checkout()
        {
            return View();
        }
    }
}