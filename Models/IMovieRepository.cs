using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesHUB.Models
{
    public interface IMovieRepository
    {
        Movie GetMovie(int Id);
        IEnumerable<Movie> GetAllMovies();
        Movie Add(Movie MovieChanges);
        Movie Update(Movie MovieChanges);
        Movie Delete(int Id);
    }
}
