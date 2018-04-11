using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Movies.DomainModel.Tests
{
    [TestClass]
    public class MoviesTests
    {
        [TestMethod]
        public void CreateMoviesObject()
        {
            var movies = new Movies();
            Assert.IsNotNull(movies);
        }

        [TestMethod]
        public void ReadAndWriteMovieProperties()
        {
            var movies = new Movies();
            Assert.AreEqual(0, movies.Actors.Count);
            Assert.AreEqual(0, movies.AllMovies.Count);
            Assert.AreEqual(0, movies.RolesByActor("Eddie Murphy").Count);
        }

        [TestMethod]
        public void InitiliseMovies()
        {
            var role = new Role();
            var movie = new Movie();
            movie.Roles.Add(role);
            var listOfMovies = new List<Movie> {movie};

            var movies = new Movies();
            movies.Initialise(listOfMovies);

            Assert.AreEqual(1, movies.AllMovies.Count);
            Assert.AreSame(movie, movies.AllMovies[0].Roles.First().Movie);
        }

        [TestMethod]
        public void GetUniqueListOfActorsFromOneMovie()
        {
            var listOfMovies = new List<Movie>
            {
                new Movie
                {
                    Roles = new HashSet<Role>
                    {
                        new Role {ActorName = "Eddie Murphy"},
                        new Role {ActorName = "Eddie Murphy"}
                    }
                }
            };

            var movies = new Movies();
            movies.Initialise(listOfMovies);

            Assert.AreEqual(1, movies.Actors.Count);
            Assert.AreEqual("Eddie Murphy", movies.Actors[0]);
        }

        [TestMethod]
        public void GetUniqueListOfActorsFromTwoMovies()
        {
            var listOfMovies = new List<Movie>
            {
                new Movie
                {
                    Roles = new HashSet<Role>
                    {
                        new Role {ActorName = "Eddie Murphy"}
                    }
                },
                new Movie
                {
                    Roles = new HashSet<Role>
                    {
                        new Role {ActorName = "Eddie Murphy"}
                    }
                }
            };

            var movies = new Movies();
            movies.Initialise(listOfMovies);

            Assert.AreEqual(1, movies.Actors.Count);
            Assert.AreEqual("Eddie Murphy", movies.Actors[0]);
        }

        [TestMethod]
        public void GetRolesForActor()
        {
            var listOfMovies = new List<Movie>
            {
                new Movie
                {
                    Roles = new HashSet<Role>
                    {
                        new Role {CharacterName = "Axel Foley", ActorName = "Eddie Murphy"}
                    }
                },
                new Movie
                {
                    Roles = new HashSet<Role>
                    {
                        new Role {CharacterName = "Donkey", ActorName = "Eddie Murphy"}
                    }
                }
            };

            var movies = new Movies();
            movies.Initialise(listOfMovies);

            Assert.AreEqual(2, movies.RolesByActor("Eddie Murphy").Count);
            Assert.AreEqual("Axel Foley", movies.RolesByActor("Eddie Murphy").ToArray()[0].CharacterName);
            Assert.AreEqual("Donkey", movies.RolesByActor("Eddie Murphy").ToArray()[1].CharacterName);
        }
    }
}
