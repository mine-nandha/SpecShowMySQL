using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecShow.Data;
using SpecShow.Models;

namespace SpecShow.Controllers
{
	public class HomeController : Controller
	{
		private readonly SpecShowContext _context;
		public readonly string _sessionName = "UserID";
		public HomeController(SpecShowContext context)
		{
			_context = context;
		}

		// GET: Home?query=Samsung
		public async Task<IActionResult> Index(string search, string sortBy)
		{
			var mobiles = await _context.Mobiles.OrderBy(m => m.MobileName).ToListAsync();
			if (!string.IsNullOrEmpty(sortBy))
			{
				if (sortBy == "name")
				{
					mobiles = await _context.Mobiles.OrderBy(m => m.MobileName).ToListAsync();
				}
				else if (sortBy == "priceLow")
				{
					mobiles = await _context.Mobiles.OrderBy(m => m.Price).ToListAsync();
				} else if (sortBy == "priceHigh")
				{
					mobiles = await _context.Mobiles.OrderByDescending(m => m.Price).ToListAsync();
				} else
				{
					mobiles = await _context.Mobiles.OrderByDescending(m => m.Rating).ToListAsync();
				}
			}
			if (!string.IsNullOrEmpty(search))
			{
				ViewData["Search"] = search;
				mobiles = mobiles.Where(m => m.MobileName.ToLower().Contains(search.ToLower())).ToList();
			}
			return View(mobiles);
		}

		// GET: Home/GetSearchSuggestions
		public JsonResult GetSearchResults(string query)
		{
			var results = _context.Mobiles
				 .Where(t => t.MobileName.Contains(query))
				 .Select(t => t.MobileName)
				 .Take(10)
				 .ToList();

			return Json(results);
		}

		// GET: Home/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Mobiles == null)
			{
				return NotFound();
			}

			var mobile = await _context.Mobiles
				.FirstOrDefaultAsync(m => m.MobileID == id);
			if (mobile == null)
			{
				return NotFound();
			}
			ViewBag.isExists =  _context.Favourites.SingleOrDefault(f => f.MobileID == id && f.UserID == HttpContext.Session.GetInt32("UserID")) != null;
			return View(mobile);
		}

		// GET: Home/Create
		public IActionResult Create()
		{
			var id = HttpContext.Session.GetInt32(_sessionName);
			if (id == 1)
			{
				return View();
			}
			else
			{
				//ViewBag.ErrorMessage = "You must be an admin to create records here.";
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: Home/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("MobileID,MobileName,Model,Price,Brand,ImageUrl,AmazonUrl,FlipkartUrl,ScreenSize,FrontCamera,BackCameras,OS,Sim,Sensors,BatteryCapacity,Colors,Variants,ReleaseDate,Weight,Material,ResistanceCertificate,ScreenType,AspectRatio,Resolution,OtherFeatures,Processor,Nanometer,GPU,AntutuScores,Bluetooth,ChargingCapacity,Rating")] Mobile mobile)
		{
			if (ModelState.IsValid)
			{
				_context.Add(mobile);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(mobile);
		}

		// GET: Home/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);
			if (userId == 1)
			{
				var mobile = await _context.Mobiles.FindAsync(id);
				return View(mobile);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: Home/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([Bind("MobileID,MobileName,Model,Price,Brand,ImageUrl,AmazonUrl,FlipkartUrl,ScreenSize,FrontCamera,BackCameras,OS,Sim,Sensors,BatteryCapacity,Colors,Variants,ReleaseDate,Weight,Material,ResistanceCertificate,ScreenType,AspectRatio,Resolution,OtherFeatures,Processor,Nanometer,GPU,AntutuScores,Bluetooth,ChargingCapacity,Rating")] Mobile mobile)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(mobile);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(mobile);
		}
	}
}
