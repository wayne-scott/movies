using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Movies.DAL;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : BaseController
    {
        public ActorController(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        // GET api/actor
        [HttpGet]
        public IActionResult Get()
        {
            var movies = MoviesRepository.GetAllMovies();
            if (movies?.Actors.Count > 0)
            {
                return new ObjectResult(movies.Actors.OrderBy(actorName => actorName).ToList());
            }

            return NotFound();
        }

        // GET api/controller/actor
        [HttpGet("{actor}")]
        public IActionResult Get(string actor)
        {
            var movies = MoviesRepository.GetAllMovies();
            if (movies?.Actors.Count > 0)
            {
                return new ObjectResult(ModelFactory.Create(movies.Actors.FirstOrDefault(actorName => actorName == actor), movies));
            }

            return NotFound();
        }
    }
}
