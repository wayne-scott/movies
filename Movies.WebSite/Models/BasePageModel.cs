﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Movies.DAL;

namespace Movies.WebSite.Models
{
    public class BasePageModel : PageModel
    {
        protected BasePageModel(IMoviesRepository moviesRepository)
        {
            MoviesRepository = moviesRepository;
            ModelFactory = new ModelFactory();
        }

        protected IMoviesRepository MoviesRepository { get; private set; }
        protected ModelFactory ModelFactory { get; private set; }
    }
}
