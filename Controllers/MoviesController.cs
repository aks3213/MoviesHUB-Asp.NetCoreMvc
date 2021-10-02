using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesHUB.Models;

namespace MoviesHUB.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MoviesController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }/*
          
        [HttpGet]
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }
        [HttpPost]
          */
        public IActionResult Index(string id)
        {
            var movies = from m in _context.Movies select m;
            if (!string.IsNullOrEmpty(id))
            {
                movies = movies.Where(s => s.Details.Contains(id) || s.Genre.Contains(id) || s.Name.Contains(id));
            }
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent from the browser of a trusted user. 
         */
        public async Task<IActionResult> Create([Bind("Id,Name,Details,Genre,ScreenShot1,ScreenShot2,ScreenShot3,ScreenShot4,MovieFile")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot1.FileName);
                string extension = Path.GetExtension(movie.ScreenShot1.FileName);
                movie.path1 =fileName= fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string sspath1 = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(sspath1, FileMode.Create))
                {
                    await movie.ScreenShot1.CopyToAsync(fileStream);
                }

                fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot2.FileName);
                extension = Path.GetExtension(movie.ScreenShot2.FileName);
                movie.path2 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string sspath2 = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(sspath2, FileMode.Create))
                {
                    await movie.ScreenShot2.CopyToAsync(fileStream);
                }

                fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot3.FileName);
                extension = Path.GetExtension(movie.ScreenShot3.FileName);
                movie.path3 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string sspath3 = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(sspath3, FileMode.Create))
                {
                    await movie.ScreenShot3.CopyToAsync(fileStream);
                }

                fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot4.FileName);
                extension = Path.GetExtension(movie.ScreenShot4.FileName);
                movie.path4 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string sspath4 = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(sspath4, FileMode.Create))
                {
                    await movie.ScreenShot4.CopyToAsync(fileStream);
                }

                fileName = Path.GetFileNameWithoutExtension(movie.MovieFile.FileName);
                extension = Path.GetExtension(movie.MovieFile.FileName);
                movie.path = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string moviepath = Path.Combine(wwwRootPath + "/movies/", fileName);
                using (var fileStream = new FileStream(moviepath, FileMode.Create))
                {
                    await movie.MovieFile.CopyToAsync(fileStream);
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Details,Genre,ScreenShot1,ScreenShot2,ScreenShot3,ScreenShot4,MovieFile")] Movie movie)
        {
            if (id != movie.Id)
            {
                _context.Entry(movie).State = EntityState.Modified;
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Movie oldMovie = await _context.Movies
                 .FirstOrDefaultAsync(m => m.Id == id);
                    if (oldMovie != null)
                    {
                        _context.Entry(oldMovie).State = EntityState.Detached;
                    }
                    //////////////////////////////////////////////////////////////////////////
                    var oldpath1 = Path.Combine(_hostEnvironment.WebRootPath, "images", oldMovie.path1);
                    if (System.IO.File.Exists(oldpath1))
                        System.IO.File.Delete(oldpath1);

                    var oldpath2 = Path.Combine(_hostEnvironment.WebRootPath, "images", oldMovie.path2);
                    if (System.IO.File.Exists(oldpath2))
                        System.IO.File.Delete(oldpath2);

                    var oldpath3 = Path.Combine(_hostEnvironment.WebRootPath, "images", oldMovie.path3);
                    if (System.IO.File.Exists(oldpath3))
                        System.IO.File.Delete(oldpath3);

                    var oldpath4 = Path.Combine(_hostEnvironment.WebRootPath, "images", oldMovie.path4);
                    if (System.IO.File.Exists(oldpath4))
                        System.IO.File.Delete(oldpath4);

                    var oldmoviepath = Path.Combine(_hostEnvironment.WebRootPath, "images", oldMovie.path);
                    if (System.IO.File.Exists(oldmoviepath))
                        System.IO.File.Delete(oldmoviepath);
                    ////////////////////////////////////////////////////////////
                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    string fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot1.FileName);
                    string extension = Path.GetExtension(movie.ScreenShot1.FileName);
                    movie.path1 = fileName = fileName + DateTime.Now.ToString("yymmoldfff") + extension;
                    string sspath1 = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(sspath1, FileMode.Create))
                    {
                        await movie.ScreenShot1.CopyToAsync(fileStream);
                    }

                    fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot2.FileName);
                    extension = Path.GetExtension(movie.ScreenShot2.FileName);
                    movie.path2 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string sspath2 = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(sspath2, FileMode.Create))
                    {
                        await movie.ScreenShot2.CopyToAsync(fileStream);
                    }

                    fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot3.FileName);
                    extension = Path.GetExtension(movie.ScreenShot3.FileName);
                    movie.path3 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string sspath3 = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(sspath3, FileMode.Create))
                    {
                        await movie.ScreenShot3.CopyToAsync(fileStream);
                    }

                    fileName = Path.GetFileNameWithoutExtension(movie.ScreenShot4.FileName);
                    extension = Path.GetExtension(movie.ScreenShot4.FileName);
                    movie.path4 = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string sspath4 = Path.Combine(wwwRootPath + "/images/", fileName);
                    using (var fileStream = new FileStream(sspath4, FileMode.Create))
                    {
                        await movie.ScreenShot4.CopyToAsync(fileStream);
                    }

                    fileName = Path.GetFileNameWithoutExtension(movie.MovieFile.FileName);
                    extension = Path.GetExtension(movie.MovieFile.FileName);
                    movie.path = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string moviepath = Path.Combine(wwwRootPath + "/movies/", fileName);
                    using (var fileStream = new FileStream(moviepath, FileMode.Create))
                    {
                        await movie.MovieFile.CopyToAsync(fileStream);
                    }

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var movie = await _context.Movies.FindAsync(id);

            var sspath1 = Path.Combine(_hostEnvironment.WebRootPath, "images", movie.path1);
            if (System.IO.File.Exists(sspath1))
                System.IO.File.Delete(sspath1);

            var sspath2 = Path.Combine(_hostEnvironment.WebRootPath, "images", movie.path2);
            if (System.IO.File.Exists(sspath2))
                System.IO.File.Delete(sspath2);

            var sspath3 = Path.Combine(_hostEnvironment.WebRootPath, "images", movie.path3);
            if (System.IO.File.Exists(sspath3))
                System.IO.File.Delete(sspath3);

            var sspath4 = Path.Combine(_hostEnvironment.WebRootPath, "images", movie.path4);
            if (System.IO.File.Exists(sspath4))
                System.IO.File.Delete(sspath4);

            var moviepath = Path.Combine(_hostEnvironment.WebRootPath, "images", movie.path);
            if (System.IO.File.Exists(moviepath))
                System.IO.File.Delete(moviepath);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Genre(string id)
        {
            var movies = from m in _context.Movies select m;
            if (!string.IsNullOrEmpty(id))
            {
                movies = movies.Where(s => s.Genre.Contains(id));
            }
            return View(movies);
        }
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }


    }
}
