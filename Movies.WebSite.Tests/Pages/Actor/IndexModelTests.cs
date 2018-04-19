using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.DAL;
using Movies.DomainModel;
using Movies.WebSite.Pages.Actor;
using NSubstitute;

namespace Movies.WebSite.Tests.Pages.Actor
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

            Assert.IsNull(indexModel.Actor);
            Assert.IsNotNull(indexModel.Actors);
            Assert.AreEqual(1, indexModel.Actors.Count);
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

            Assert.IsNull(indexModel.Actor);
            Assert.IsNull(indexModel.Actors);
            Assert.AreEqual("No actors found?", indexModel.Message);
            repository.Received(1).GetAllMovies();
        }

        [TestMethod]
        public void OnGetWithActorName()
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
            indexModel.OnGet("Eddie Murphy");

            Assert.IsNotNull(indexModel.Actor);
            Assert.IsNull(indexModel.Actors);
            Assert.AreEqual("Eddie Murphy", indexModel.Actor.Name);
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

            Assert.IsNull(indexModel.Actor);
            Assert.IsNull(indexModel.Actors);
            Assert.AreEqual("Fred not found?", indexModel.Message);
            repository.Received(1).GetAllMovies();
        }
    }
}
