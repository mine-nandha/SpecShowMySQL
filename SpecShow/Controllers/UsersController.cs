using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecShow.Data;
using SpecShow.Models;

namespace SpecShow.Controllers
{
	public class UsersController : Controller
	{
		private readonly SpecShowContext _context;
		public readonly string _sessionName = "UserID";
		public UsersController(SpecShowContext context)
		{
			_context = context;
		}

		// GET: Users
		public async Task<IActionResult> Index()
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);
			if (userId != null)
			{
				if (userId != 1)
				{
					return RedirectToAction("Details",new { id = userId });
				}
				return View(await _context.Users.ToListAsync());
			}
			return RedirectToAction(nameof(Login));
		}

		// GET: Users/Login
		public IActionResult Login()
		{
			if (HttpContext.Session.GetInt32(_sessionName) != null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		// POST: Users/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([Bind("UserName,Password")] User user)
		{
			if (await _context.Users.SingleOrDefaultAsync(m => m.UserName == user.UserName) != null)
			{
				var loggedUser = await _context.Users.SingleOrDefaultAsync(m => m.UserName == user.UserName && m.Password == user.Password);
				if (loggedUser != null)
				{
					HttpContext.Session.SetInt32(_sessionName, loggedUser.UserID);
					return RedirectToAction("Index", "Home");
				}
				ViewBag.ErrorMessage = "Password is Wrong";
				return View();
			}
			ViewBag.ErrorMessage = "User Not Found";
			return View();
		}

		// POST: Users/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp([Bind("UserName,FullName,UserEmail,Password")] User user)
		{
			if (await _context.Users.SingleOrDefaultAsync(u => u.UserName == user.UserName) == null)
			{
				if (ModelState.IsValid)
				{
					_context.Add(user);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				ViewBag.ErrorMessage = "ModelState.IsValid=False";
				return View();
			}
			ViewBag.ErrorMessage = "Username already taken.";
			return View(user);
		}

		// GET: Users/Logout
		public IActionResult Logout()
		{
			if (HttpContext.Session.GetInt32(_sessionName) != null)
			{
				HttpContext.Session.Remove(_sessionName);
			}
			return RedirectToAction("Index", "Home");
		}

		// GET: Users/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);

			if (userId != null)
			{
				if (userId != 1)
				{
					if (userId != id)
					{
						//ViewBag.ErrorMessage = "You're not allowed to edit the data of others";
						return RedirectToAction("Details", "Users");
					}
				}
				var user = await _context.Users.FindAsync(id);
				return View(user);
			}
			return RedirectToAction("Login");
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,FullName,UserEmail,Password")] User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(user);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserExists(user.UserID))
					{
						//ViewBag.ErrorMessage = "No Such User Found.";
						return RedirectToAction("Index", "Home");
					}
					else
					{
						//ViewBag.ErrorMessage = "Something went wrong. Contact Admin.";
						return RedirectToAction("Index", "Home");
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		// GET: Users/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);

			var user = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);

			return View(user);
		}

		// GET: Users/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);

			if (userId != null)
			{
				if (userId != 1 && userId != id)
				{
					//ViewBag.ErrorMessage = "You're not allowed to delete the data of others";
					return RedirectToAction("Details", "Users");
				}
				var user = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
				return View(user);
			}
			return RedirectToAction("Login");
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userId = HttpContext.Session.GetInt32(_sessionName);

			if (userId != null)
			{
				if (userId != 1 && userId != id)
				{
					//ViewBag.ErrorMessage = "You're not allowed to delete the data of others";
					return RedirectToAction("Details", "Users");
				}
				var user = await _context.Users.FindAsync(id);
				if (user != null)
				{
					_context.Users.Remove(user);
				}
				await _context.SaveChangesAsync();
				if (HttpContext.Session.GetInt32(_sessionName) != 1)
				{
					return RedirectToAction("Logout");
				}
				return RedirectToAction("Index");
			}
			return RedirectToAction("Login");
		}

		private bool UserExists(int id)
		{
			return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
		}
	}
}
