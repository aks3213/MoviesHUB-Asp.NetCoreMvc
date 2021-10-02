using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHUB.Models
{
    public class SQLMovieRepository:IMovieRepository
    {
        private readonly AppDbContext context;
        public SQLMovieRepository(AppDbContext context)
        {
            this.context = context;
        }
        Movie IMovieRepository.Add(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return movie;
        }
        Movie IMovieRepository.Delete(int Id)
        {
            Movie movie = context.Movies.Find(Id);
            if (movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
            return movie;
        }
        IEnumerable<Movie> IMovieRepository.GetAllMovies()
        {
            return context.Movies;
        }

        Movie IMovieRepository.GetMovie(int id)
        {
            return context.Movies.Find(id);
        }

        Movie IMovieRepository.Update(Movie movieChanges)
        {
            var movie = context.Movies.Attach(movieChanges);
            movie.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return movieChanges;
        }
    }
}
