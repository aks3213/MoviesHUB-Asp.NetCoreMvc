using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesHUB.Models;

namespace MoviesHUB.Controllers
{
    [AllowAnonymous]
    public class GenreController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GenreController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Action()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Action")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }

        public async Task<IActionResult> Adventure()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Adventure")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }

        public async Task<IActionResult> Thriller()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Thriller")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }

        public async Task<IActionResult> Romance()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Romance")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }

        public async Task<IActionResult> Comedy()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Comedy")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }
        public async Task<IActionResult> Animated()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Animated")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }
        public async Task<IActionResult> Crime()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Crime")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }
        public async Task<IActionResult> Horror()
        {
            List<Movie> allmovies = await _context.Movies.ToListAsync();
            List<Movie> neededMovie = new List<Movie>();

            foreach (var item in allmovies)
            {
                if (item.Genre == "Horror")
                {
                    neededMovie.Add(item);
                }
            }
            return View(neededMovie);
        }
    }
}
