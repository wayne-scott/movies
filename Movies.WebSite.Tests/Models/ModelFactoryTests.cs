using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.DomainModel;
using Movies.WebSite.Models;

namespace Movies.WebSite.Tests.Models
{
    [TestClass]
    public class ModelFactoryTests
    {
        [TestMethod]
        public void CreateObject()
        {
            var modelFactory = new ModelFactory();
            Assert.IsNotNull(modelFactory);
        }

        [TestMethod]
        public void CreateActor()
        {
            var listOfMovies = new List<Movie>
            {
                new Movie
                {
                    Name = "Beverly Hills Cop",
                    Roles = new HashSet<Role>
                    {
                        new Role {CharacterName = "Axel Foley", ActorName = "Eddie Murphy"}
                    }
                },
                new Movie
                {
                    Name = "Shrek",
                    Roles = new HashSet<Role>
                    {
                        new Role {CharacterName = "Donkey", ActorName = "Eddie Murphy"}
                    }
                }
            };
            var movies = new DomainModel.Movies();
            movies.Initialise(listOfMovies);

            var modelFactory = new ModelFactory();
            var actor = modelFactory.Create("Eddie Murphy", movies);

            Assert.AreEqual("Eddie Murphy", actor.Name);
            Assert.IsTrue(actor.HasRoles);
            Assert.AreEqual(2, actor.Roles.Count);
            Assert.AreEqual("Beverly Hills Cop", actor.Roles[0].Movie.Name);
            Assert.AreEqual("Shrek", actor.Roles[1].Movie.Name);
        }

        [TestMethod]
        public void CreateListOfActors()
        {
            var movies = new DomainModel.Movies();

            var modelFactory = new ModelFactory();
            var actors = modelFactory.Create(new List<string>{ "Wil Wheaton", "Eddie Murphy", "Judge Reinhold" }, movies);

            Assert.AreEqual(3, actors.Count);
            Assert.AreEqual("Eddie Murphy", actors[0].Name);
            Assert.AreEqual("Judge Reinhold", actors[1].Name);
            Assert.AreEqual("Wil Wheaton", actors[2].Name);
        }
    }
}
