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
	public class FavouritesController : Controller
	{
		private readonly SpecShowContext _context;
		private readonly string _sessionName = "UserID";

		public FavouritesController(SpecShowContext context)
		{
			_context = context;
		}

		// GET: Favourites
		public async Task<IActionResult> Index()
		{
			int? loggedUserId = HttpContext.Session.GetInt32(_sessionName);
			if (loggedUserId != null)
			{
                var specShowContext = await _context.Favourites.Where(f => f.UserID == loggedUserId).ToListAsync();
                if (specShowContext != null)
                {
                    foreach (var f in specShowContext)
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
                        f.Mobile = await _context.Mobiles.FindAsync(f.MobileID);
#pragma warning restore CS8601 // Possible null reference assignment.
                    }
                }
                return View(specShowContext);
            }
			return RedirectToAction("Index", "Users");
		}

		[HttpGet]
		public IActionResult GetCount()
		{
			int? loggedUserId = HttpContext.Session.GetInt32(_sessionName);
			int count = _context.Favourites.Where(f=> f.UserID == loggedUserId).Count();
			return Json(count);
		}


		[HttpPost]
		public async Task<IActionResult> AddFavourite(int mobileId)
		{
			int? loggedUserId = HttpContext.Session.GetInt32(_sessionName);

			if (loggedUserId == null)
			{
				//ViewBag.ErrorMessage = "You need to login if you need to do that action.";
				return RedirectToAction("Index", "Home");
			}
			Favourite favourite = new Favourite();
			favourite.UserID = (int)loggedUserId;
			favourite.MobileID = mobileId;
			_context.Favourites.Add(favourite);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		[HttpPost]
		public async Task<IActionResult> RemoveFavourite(int favouritesId)
		{
			var favourite = await _context.Favourites.FindAsync(favouritesId);
			// Remove the favorite from the database
			if (favourite != null)
			{
				_context.Favourites.Remove(favourite);
				await _context.SaveChangesAsync();
			}
			return Json(new { success = true });
		}

	}
}
