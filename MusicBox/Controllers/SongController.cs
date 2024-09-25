
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicBox.Models;
using MusicBox.Repository;
using MusicBox.Filters;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicBox.Services;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace MusicBox.Controllers
{
	[Culture]
	public class SongController : Controller
    {
        private readonly IRepository<Songs> repoS;
        private readonly IRepository<Users> repoU;
        private readonly IRepository<Genres> repoG;
        IWebHostEnvironment _appEnvironment;
		readonly ILangRead _langRead;

        public SongController(ILangRead langRead, IRepository<Songs> rs, IRepository<Users> ru, IRepository<Genres> rg, IWebHostEnvironment appEnvironment)
        {
           
            repoS = rs;
            repoU = ru;
            repoG = rg;
            _appEnvironment = appEnvironment;		
			_langRead = langRead;
		}

        // GET: Song
        //    

        public async Task<IActionResult> Index(string singer, int genre = 0, int page = 1, SortState sortOrder = SortState.SingerAsc)
		{
			const int pageSize = 4;

			//фильтрация
			IQueryable<Songs> songs = repoS.GetFilteredSongs(singer, genre, sortOrder);
			var count = await songs.CountAsync();
			
			var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
			var genres = await repoG.ToList();
			//// формируем модель представления
			IndexViewModel viewModel = new IndexViewModel(
				items,
			new PageViewModel(count, page, pageSize),
			new FilterViewModel(genres.ToList(), genre, singer),
			new SortViewModel(sortOrder)
			);

			ViewData["SingerSort"] = sortOrder == SortState.SingerAsc ? SortState.SingerDesc : SortState.SingerAsc;
			ViewData["SongSort"] = sortOrder == SortState.SongAsc ? SortState.SongDesc : SortState.SongAsc;


			HttpContext.Session.SetString("path", Request.Path);
            var id = HttpContext.Session.GetString("Id");
            if (id != null)
            {
                var user = await repoU.GetById(int.Parse(id));
                if (user != null && user.Login == "admin")
                {
                    ViewBag.IsAdmin = true;
                }
            }
			return View(viewModel);
			//var s = await repoS.ToList();
			//return View(songs);
			//return View(await songs.ToListAsync());
		}



        public async Task<IActionResult> IndexAdmin()

        {
            IEnumerable<Songs> model = await repoS.ToList();
            return View(model);
        }
        // GET: Song/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await repoS.GetById((int)id);

            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }
        public async Task<bool> SongExistsAsync(Songs song)
        {
			
			return await repoS.Query().AnyAsync(m => m.name == song.name && m.singer == song.singer);
			//return repoS.ToList().Result.Where(m => m.name == song.name &&
   //             m.singer == song.singer).ToList().FirstOrDefault() != null;
        }

        // GET: Song/Create
        public async Task<IActionResult> Create()
        {
			HttpContext.Session.SetString("path", Request.Path);
			var genreList = await repoG.ToList();
            ViewData["GenreId"] = new SelectList(genreList, "Id", "name");

            return View();

        }

        // POST: Song/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,singer,runtime,size,file,pic,Datetime,user,genre")] Songs s, int UserId, int GenreId, IFormFile? uploadedPreview, IFormFile? uploadedHref)
        {
            if (ModelState.IsValid)
            {

                if (await SongExistsAsync(s))
                {

                    return View("~/Views/Song/Error.cshtml");
                }
                if (uploadedPreview != null)
                {
                    string path = "/img/" + uploadedPreview.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedPreview.CopyToAsync(fileStream);
                    }
                    s.pic = path;

                }
                if (uploadedHref != null)
                {
                    string path1 = "/mp3/" + uploadedHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
                    {
                        await uploadedHref.CopyToAsync(fileStream);
                    }
                    s.file = path1;
                }
            }
            var id = HttpContext.Session.GetString("Id");
            var user = await repoU.GetById(int.Parse(id));
            if (ModelState.IsValid)

            {
                s.genre = await repoG.GetById(GenreId);
                s.user = user;
                s.Datetime = DateTime.Now;

                await repoS.Create(s);
                await repoS.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(s);
        }

        // GET: Song/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
           
            var genreList = await repoG.ToList();
            ViewData["GenreId"] = new SelectList(genreList, "Id", "name");

            var song = await repoS.GetById(id); // Получаем песню по id
            if (song == null)
            {
                return NotFound();
            }

            return View(song); // Передаем объект песни в представление

           
        }
        // POST: Song/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,singer,runtime,size,file,pic,Datetime, user, genre")] Songs song, int UserId, int GenreId, IFormFile? uploadedPreview, IFormFile? uploadedHref)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var editSong = await repoS.GetById(id);
                if (uploadedPreview != null)
                {
                    string path = "/img/" + uploadedPreview.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedPreview.CopyToAsync(fileStream);
                    }
                    editSong.pic = path;
                }
                if (uploadedHref != null)
                {
                    string path1 = "/mp3/" + uploadedHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path1, FileMode.Create))
                    {
                        await uploadedHref.CopyToAsync(fileStream);
                    }
                    editSong.file = path1;
                }
                editSong.name = song.name;
                editSong.singer = song.singer;
                editSong.runtime = song.runtime;
                editSong.size = song.size;
                editSong.Datetime = DateTime.Now;
                editSong.user = song.user;
                editSong.genre = await repoG.GetById(GenreId);
                ViewBag.GenreId = GenreId;
                try
                {
                    repoS.Update(editSong);
                    await repoS.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (song.Id == 0)
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
            return View(song);
        }

        // GET: Song/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var song = (await repoS.ToList()).FirstOrDefault(m => m.Id == id);

            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var song = (await repoS.ToList()).FirstOrDefault(e => e.Id == id);
            if (song != null)
            {
                await repoS.Delete(id);

            }

            await repoS.Save();
            return RedirectToAction(nameof(Index));
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
			option.Expires = DateTime.Now.AddDays(10); 
			Response.Cookies.Append("lang", lang, option); 
			return Redirect(returnUrl);
		}

	}

}

	