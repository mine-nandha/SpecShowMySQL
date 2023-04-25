using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecShow.Data;
using SpecShow.Models;

namespace SpecShow.Controllers
{
	public class CompareController : Controller
	{
		private readonly SpecShowContext _context;
		public CompareController(SpecShowContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(CompareViewModel compare)
		{
			var first = await _context.Mobiles.FirstOrDefaultAsync(m => m.MobileName == compare.FirstMobile.MobileName);
			var second = await _context.Mobiles.FirstOrDefaultAsync(m => m.MobileName == compare.SecondMobile.MobileName);
			CompareViewModel c = new CompareViewModel();
			if (first != null && second != null)
			{
				c.FirstMobile = first;
				c.SecondMobile = second;
			}
			return View(c);
		}
	}
}