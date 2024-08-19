using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBox.Filters;
using MusicBox.Models;
using MusicBox.Repository;
using MusicBox.Services;
using System.Diagnostics;

namespace MusicBox.Controllers
{
	[Culture]
	public class HomeController : Controller
    {
        private readonly IRepository<Users> repo;
		readonly ILangRead _langRead;
		private readonly SongContext _context;

		public HomeController(SongContext context, ILangRead langRead, IRepository<Users> r)
		{
			_context = context;
			_langRead = langRead;
			repo = r;
		}

		//public HomeController(IRepository<Users> r)
  //      {
  //          repo = r;
  //      }


        public IActionResult Index()
        {
			HttpContext.Session.SetString("path", Request.Path);
			return View();
        }


        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		public ActionResult ChangeCulture(string lang)
		{
			string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

			// Список культур
			List<string> cultures = new List<string>() { "ru", "en", "uk", "es" };
			if (!cultures.Contains(lang))
			{
				lang = "ru";
			}

			CookieOptions option = new CookieOptions();
			option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
			Response.Cookies.Append("lang", lang, option); // создание куки
			return Redirect(returnUrl);
		}

	}
}
