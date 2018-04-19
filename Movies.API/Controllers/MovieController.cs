using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using System.Linq;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : BaseController
    {
        public MovieController(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        // GET api/movie
        [HttpGet]
        public IActionResult Get()
        {
            var movies = MoviesRepository.GetAllMovies();
            if (movies?.AllMovies.Count > 0)
            {
                return new ObjectResult(movies.AllMovies.Select(m => m.Name).OrderBy(movieName => movieName).ToList());
            }

            return NotFound();
        }

        // GET api/controller/actor
        [HttpGet("{movie}")]
        public IActionResult Get(string movie)
        {
            var movies = MoviesRepository.GetAllMovies();
            if (movies?.AllMovies.Count > 0)
            {
                return new ObjectResult(movies.AllMovies.FirstOrDefault(m => m.Name == movie));
            }

            return NotFound();
        }
    }
}
