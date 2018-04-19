using Microsoft.AspNetCore.Mvc;
using Movies.DAL;
using Movies.WebSite.Models;

namespace Movies.API.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IMoviesRepository moviesRepository)
        {
            MoviesRepository = moviesRepository;
            ModelFactory = new ModelFactory();
        }

        protected IMoviesRepository MoviesRepository { get; private set; }
        protected ModelFactory ModelFactory { get; private set; }
    }
}
