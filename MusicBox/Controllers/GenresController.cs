using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using MusicBox.Filters;
using MusicBox.Models;
using MusicBox.Repository;
using MusicBox.Services;

namespace MusicBox.Controllers
{
	[Culture]
	public class GenresController : Controller
    {
        private readonly IRepository<Songs> repoS;
        private readonly IRepository<Users> repoU;
        private readonly IRepository<Genres> repoG;
        IWebHostEnvironment _appEnvironment;
        public GenresController(IRepository<Songs> rs, IRepository<Users> ru, IRepository<Genres> rg, IWebHostEnvironment appEnvironment)
        {
            repoS = rs;
            repoU = ru;
            repoG = rg;
            _appEnvironment = appEnvironment;
        }

        private readonly SongContext _context;

		readonly ILangRead _langRead;

		public GenresController(SongContext context, ILangRead langRead)
		{
			_context = context;
			_langRead = langRead;
		}


		public async Task<IActionResult> Index()
        {
            var genres = await repoG.ToList();
            //ViewBag.GenresList = new SelectList(genres, "Id","Name");
            return View(genres);
        }


        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = (await repoG.ToList()).FirstOrDefault(m => m.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name")] Genres g)
        {
            if (ModelState.IsValid)
            {
                var id = HttpContext.Session.GetString("Id");
                var user = await repoU.GetById(int.Parse(id));

                await repoG.Create(g);
                await repoG.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(g);
        }



        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genres = await repoG.GetById(id);
            if (genres == null)
            {
                return NotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name")] Genres genres)
        {
            if (id != genres.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repoG.Update(genres);
                    await repoG.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenresExists(genres.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genres);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = (await repoG.ToList()).FirstOrDefault(m => m.Id == id);


            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = (await repoG.ToList()).FirstOrDefault(m => m.Id == id);
            if (genre != null)
            {
                await repoG.Delete(id);
            }

            await repoG.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GenresExists(int id)
        {
            return _context.genres.Any(e => e.Id == id);
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
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



    
