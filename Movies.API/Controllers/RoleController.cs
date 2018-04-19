using Microsoft.AspNetCore.Mvc;
using Movies.DAL;

namespace Movies.API.Controllers
{
    [Route("api/actor/[controller]")]
    public class RoleController : BaseController
    {
        public RoleController(IMoviesRepository moviesRepository) : base(moviesRepository) {}

        // GET api/actor/role
        [HttpGet]
        public IActionResult Get()
        {
            var movies = MoviesRepository.GetAllMovies();
            if (movies != null)
            {
                return new ObjectResult(ModelFactory.Create(movies.Actors, movies));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
