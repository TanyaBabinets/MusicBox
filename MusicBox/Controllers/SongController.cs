using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicBox.Models;
using MusicBox.Repository;
using System.Linq;
using System.IO;

namespace MusicBox.Controllers
{
    public class SongController : Controller
    {
        private readonly IRepository<Songs> repoS;
        private readonly IRepository<Users> repoU;
        private readonly IRepository<Genres> repoG;
        IWebHostEnvironment _appEnvironment;

        public SongController(IRepository<Songs> rs, IRepository<Users> ru, IRepository<Genres> rg, IWebHostEnvironment appEnvironment)
        {
            repoS = rs;
            repoU = ru;
            repoG = rg;
            _appEnvironment = appEnvironment;
        }

        // GET: Song
        //    
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetString("Id");
            if (id != null)
            {
                var user = await repoU.GetById(int.Parse(id));
                if (user != null && user.Login == "admin")
                {
                    ViewBag.IsAdmin = true;
                }
            }

            var songs = await repoS.ToList();
            return View(songs);
        }


        public async Task<IActionResult> IndexAdmin()
        {
            var model = await repoS.ToList();
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
        public bool SongExists(Songs song)
        {

            // Проверяем, есть ли song в базе данных
            return repoS.ToList().Result.Where(m => m.name == song.name &&
                m.singer == song.singer).ToList().FirstOrDefault() != null;
        }

        // GET: Song/Create
        public async Task<IActionResult> Create()
        {
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

                if (SongExists(s))
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
            //var song = await repoS.GetById(id); // Получаем песню по id
            //if (song == null)
            //{
            //	return NotFound();
            //}
            var genreList = await repoG.ToList();
            ViewData["GenreId"] = new SelectList(genreList, "Id", "name");

            var song = await repoS.GetById(id); // Получаем песню по id
            if (song == null)
            {
                return NotFound();
            }

            return View(song); // Передаем объект песни в представление

            //return View(id);
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
    }

  }

	