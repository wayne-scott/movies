using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using Movies.WebSite.Models;

namespace Movies.WebSite.Pages.Movie
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        public IList<string> Movies { get; set; }
        public DomainModel.Movie Movie { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(string movie)
        {
            var movies = MoviesRepository.GetAllMovies();
            if (!string.IsNullOrEmpty(movie))
            {
                if (movies?.AllMovies.Count > 0)
                {
                    Movie = movies.AllMovies.FirstOrDefault(m => m.Name == movie);
                }

                if (Movie == null)
                {
                    Message = $"{movie} not found?";
                }
            }
            else
            {
                if (movies?.AllMovies.Count > 0)
                {
                    Movies = movies.AllMovies.Select(m => m.Name).OrderBy(movieName => movieName).ToList();
                }

                if (Movies == null)
                {
                    Message = "No movies found?";
                }
            }
        }
    }
}