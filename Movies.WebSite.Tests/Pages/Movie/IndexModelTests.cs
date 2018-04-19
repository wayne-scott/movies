using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.DAL;
using Movies.DomainModel;
using Movies.WebSite.Pages.Movie;
using NSubstitute;

namespace Movies.WebSite.Tests.Pages.Movie
{
    [TestClass]
    public class IndexModelTests
    {
        [TestMethod]
        public void OnGet()
        {
            var repository = Substitute.For<IMoviesRepository>();
            var movies = new DAL.MoviesContext();
            movies.Initialise(new List<DomainModel.Movie>
            {
                new DomainModel.Movie
                {
                    Name = "Beverly Hills Cop",
                    Roles = new HashSet<Role> {new Role {CharacterName = "Axel Foley", ActorName = "Eddie Murphy"}}
                }
            });
            repository.GetAllMovies().Returns(movies);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet(string.Empty);

            Assert.IsNull(indexModel.Movie);
            Assert.IsNotNull(indexModel.Movies);
            Assert.AreEqual(1, indexModel.Movies.Count);
            Assert.IsTrue(string.IsNullOrEmpty(indexModel.Message));
            repository.Received(1).GetAllMovies();
        }

        [TestMethod]
        public void OnGetNoData()
        {
            var repository = Substitute.For<IMoviesRepository>();
            repository.GetAllMovies().Returns(new DAL.MoviesContext());

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet(string.Empty);

            Assert.IsNull(indexModel.Movie);
            Assert.IsNull(indexModel.Movies);
            Assert.AreEqual("No movies found?", indexModel.Message);
            repository.Received(1).GetAllMovies();
        }

        [TestMethod]
        public void OnGetWithMovieName()
        {
            var repository = Substitute.For<IMoviesRepository>();
            var movies = new DAL.MoviesContext();
            movies.Initialise(new List<DomainModel.Movie>
            {
                new DomainModel.Movie
                {
                    Name = "Beverly Hills Cop",
                    Roles = new HashSet<Role> {new Role {CharacterName = "Axel Foley", ActorName = "Eddie Murphy"}}
                }
            });
            repository.GetAllMovies().Returns(movies);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet("Beverly Hills Cop");

            Assert.IsNotNull(indexModel.Movie);
            Assert.IsNull(indexModel.Movies);
            Assert.AreEqual("Beverly Hills Cop", indexModel.Movie.Name);
            Assert.IsTrue(string.IsNullOrEmpty(indexModel.Message));
            repository.Received(1).GetAllMovies();
        }

        [TestMethod]
        public void OnGetWithActorNameNoMatch()
        {
            var repository = Substitute.For<IMoviesRepository>();
            var movies = new DAL.MoviesContext();
            movies.Initialise(new List<DomainModel.Movie>
            {
                new DomainModel.Movie
                {
                    Name = "Beverly Hills Cop",
                    Roles = new HashSet<Role> {new Role {CharacterName = "Axel Foley", ActorName = "Eddie Murphy"}}
                }
            });
            repository.GetAllMovies().Returns(movies);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet("Fred");

            Assert.IsNull(indexModel.Movie);
            Assert.IsNull(indexModel.Movies);
            Assert.AreEqual("Fred not found?", indexModel.Message);
            repository.Received(1).GetAllMovies();
        }
    }
}
