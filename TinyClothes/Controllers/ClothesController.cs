using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class ClothesController : Controller
    {
        private readonly StoreContext _context;

        public ClothesController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAll(int? page)
        {
            const int PageSize = 2;

            // Null-coalescing operator: ??
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            int pageNumber = page ?? 1;
            ViewData["CurrentPage"] = pageNumber;

            int maxPage = await GetMaxPage(PageSize);
            ViewData["MaxPage"] = maxPage;

            List<Clothing> clothes = await ClothingDB.GetClothingByPage(_context, pageNum: pageNumber, pageSize: PageSize);
            return View(clothes);
        }

        private async Task<int> GetMaxPage(int PageSize)
        {
            int numProducts = await ClothingDB.GetNumClothing(_context);

            int maxPage = Convert.ToInt32(Math.Ceiling((double)numProducts / PageSize));
            return maxPage;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Add(c, _context);

                // TempData lasts for one redirect
                TempData["Message"] = $"{c.Title} added successfully";
                return RedirectToAction("ShowAll");
            }
            //Return same view with error messages
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);
            if (c == null)
            {
                // HTTP 404 Not Found
                return NotFound();
            }


            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDB.Edit(c, _context);
                ViewData["Message"] = c.Title + " Updated Successfully";
            }

            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);
            if (c == null)
            {
                return NotFound();
            }

            return View(c);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Clothing c = await ClothingDB.GetClothingById(id, _context);
            await ClothingDB.Delete(c, _context);
            TempData["Message"] = c.Title + " Deleted Successfully";

            return RedirectToAction(nameof(ShowAll));
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(SearchCriteria search)
        {
            //Not yet sent to DB
            IQueryable<Clothing> allClothes =
                (from c in _context.Clothing
                 select c);

            //Where Price > MaxPrice
            if (search.MinPrice.HasValue)
            {
                //Adds to allClothes Query
                allClothes =
                    (from c in allClothes
                     where c.Price > search.MinPrice
                     select c);
            }

            //Where Price < MaxPrice
            if (search.MaxPrice.HasValue)
            {
                allClothes =
                    (from c in allClothes
                     where c.Price < search.MaxPrice
                     select c);
            }

            if (!string.IsNullOrWhiteSpace(search.Size))
            {
                allClothes =
                    (from c in allClothes
                     where c.Size == search.Size
                     select c);
            }

            if (!string.IsNullOrWhiteSpace(search.Type))
            {
                allClothes =
                    (from c in allClothes
                     where c.Type == search.Type
                     select c);
            }

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                allClothes =
                    (from c in allClothes
                     where c.Title.Contains(search.Title)
                     select c);
            }


            search.Results = await allClothes.ToListAsync();

            return View(search);
        }
    }
}