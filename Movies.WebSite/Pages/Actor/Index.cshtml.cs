using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using Movies.WebSite.Models;

namespace Movies.WebSite.Pages.Actor
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        public IList<string> Actors { get; set; }
        public Models.Actor Actor { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(string actor)
        {
            var movies = MoviesRepository.GetAllMovies();
            if (!string.IsNullOrEmpty(actor))
            {
                if (movies?.Actors.Count > 0)
                {
                    Actor = ModelFactory.Create(movies.Actors.FirstOrDefault(actorName => actorName == actor), movies);
                }

                if (Actor == null)
                {
                    Message = $"{actor} not found?";
                }
            }
            else
            {
                if (movies?.Actors.Count > 0)
                {
                    Actors = movies.Actors.OrderBy(actorName => actorName).ToList();
                }

                if (Actors == null)
                {
                    Message = "No actors found?";
                }
            }
        }
    }
}