using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBox.Models;
using MusicBox.Repository;
using System.Security.Cryptography;
using System.Text;
using MusicBox.Filters;
using MusicBox.Services;


namespace MusicBox.Controllers
{
	[Culture]
	public class AccountController : Controller
    {

        private readonly IRepository<Users> repo;
		private readonly SongContext _context;

		readonly ILangRead _langRead;

		public AccountController(SongContext context, ILangRead langRead, IRepository<Users> r)
		{
			_context = context;
			_langRead = langRead;
			repo = r;
		}

		//public AccountController(IRepository<Users> r)
  //      {
  //          repo = r;
  //      }

		public ActionResult Login()
        {
			HttpContext.Session.SetString("path", Request.Path);
			return View();
        }
    
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel log)
        {
            var userList = await repo.ToList();
            var checkUser = userList.Where(e => e.Login == log.Login).FirstOrDefault();		
			if (checkUser == null)
            {
				ModelState.AddModelError("", "Такого пользователя нет");
					return View(log);
			}

			if (ModelState.IsValid)
            {
				
				if (repo.ToList().Result.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var users = repo.ToList().Result.Where(a => a.Login == log.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                var user = users.First();
                string? salt = user.Salt;
                byte[] password = Encoding.Unicode.GetBytes(salt + log.Password);
                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(log);
                }
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Id", user.Id.ToString());
				if (user.Login == "admin")
				{                  
                    return Approval();
                 
				}
				else if (!user.IsActivated)
				{
					ModelState.AddModelError("", "Еще не подтвержден");
					return View(log);
				}
				return RedirectToAction("Create", "Song");
            }
            return View(log);
        }

        public IActionResult Register()
		{
			HttpContext.Session.SetString("path", Request.Path);
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
			var userList = await repo.ToList();
			var checkUser = userList.Where(e => e.Login == reg.Login).FirstOrDefault();

            if (checkUser != null)
            {
                ModelState.AddModelError("Login", "Такой логин уже существует");
                return View(reg);
            }
           

            if (ModelState.IsValid)
            {
                Users user = new Users();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.Email = reg.Email; 
             
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);
                var md5 = MD5.Create();

                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                user.IsActivated = false;
                await repo.Create(user);
                await repo.Save();
                
                // return RedirectToAction("Index", "Home");
                return RedirectToAction(nameof(Login));
            }

            return View(reg);
        }

        public IActionResult Create()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Login,Email,Password")] Users user)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(user);
                await repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public IActionResult Approval()
        {
			var usersToApprove = repo.ToList().Result.Where(e => e.IsActivated == false).ToList();

			return View("Approval", usersToApprove);
		}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Approval(string login)
        //{

        //    var usersToApprove = repo.ToList().Result.Where(e => e.IsActivated == false).ToList();

        //    return View("Approval", usersToApprove);
        //}
      

        public async Task<IActionResult> ApproveUser(int userId)

        {
            var user = await repo.GetById(userId);

            if (user != null)
            {
               user.IsActivated=true;
                repo.Update(user);
                await repo.Save();
                
            }
			var usersToApprove = repo.ToList().Result.Where(e => e.IsActivated == false).ToList();

			return View("Approval", usersToApprove);
			
        }


		public ActionResult ChangeCulture(string lang)
		{
			string? returnUrl = HttpContext.Session.GetString("path") ?? "/Song/Index";

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




