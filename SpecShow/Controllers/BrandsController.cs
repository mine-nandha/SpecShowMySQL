using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpecShow.Data;
using SpecShow.Models;

namespace SpecShow.Controllers
{
    public class BrandsController : Controller
    {
        private readonly SpecShowContext _context;

        public BrandsController(SpecShowContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mobiles.Select(m=>m.Brand).Distinct().ToListAsync());
        }

		// GET: Brands/Samsung
		public IActionResult Samsung()
		{
			return RedirectToAction("Index","Home", new { search = "Samsung"});
		}
		// GET: Brands/Samsung
		public IActionResult Apple()
		{
			return RedirectToAction("Index","Home", new { search = "Apple" });
		}
		// GET: Brands/Samsung
		public IActionResult Nokia()
		{
			return RedirectToAction("Index","Home", new { search = "Nokia" });
		}
		// GET: Brands/Samsung
		public IActionResult Redmi()
		{
			return RedirectToAction("Index","Home", new { search = "Redmi" });
		}
		// GET: Brands/Samsung
		public IActionResult OnePlus()
		{
			return RedirectToAction("Index","Home", new { search = "OnePlus" });
		}
		// GET: Brands/Samsung
		public IActionResult Vivo()
		{
			return RedirectToAction("Index","Home", new { search = "Vivo" });
		}
		// GET: Brands/Samsung
		public IActionResult Oppo()
		{
			return RedirectToAction("Index","Home", new { search = "Oppo" });
		}
		// GET: Brands/Samsung
		public IActionResult Realme()
		{
			return RedirectToAction("Index","Home", new { search = "Realme" });
		}


	}
}
