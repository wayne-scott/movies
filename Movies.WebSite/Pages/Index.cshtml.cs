using System.Collections.Generic;
using Movies.DAL;
using Movies.WebSite.Models;

namespace Movies.WebSite.Pages
{
    public class IndexModel : BasePageModel
    {
        public List<Models.Actor> Actors { get; private set; }

        public bool HaveActors => Actors.Count > 0;

        public IndexModel(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        public void OnGet()
        {
            var movies = MoviesRepository.GetAllMovies();

            Actors = movies != null ? ModelFactory.Create(movies.Actors, movies) : new List<Models.Actor>();
        }
    }
}
