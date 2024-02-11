using Microsoft.AspNetCore.Mvc;
using MusicBox.Models;
using MusicBox.Repository;
using System.Diagnostics;

namespace MusicBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Users> repo;

        public HomeController(IRepository<Users> r)
        {
            repo = r;
        }
        public IActionResult Index()
        {
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
    }
}
