using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Movies.DomainModel.Tests
{
    [TestClass]
    public class MovieTests
    {
        #region Test Data
        private const string VALID_MOVIE = "{'name':'Beverly Hills Cop','roles':[{'name':'Axel Foley','actor':'Eddie Murphy'}]}";
        private const string MOVIE_NULL_NAME = "{'roles':[{'name':'Axel Foley','actor':'Eddie Murphy'}]}";
        private const string MOVIE_EMPTY_NAME = "{'name':'','roles':[{'name':'Axel Foley','actor':'Eddie Murphy'}]}";
        private const string MOVIE_DUPLICATE_ROLE = "{'name':'Beverly Hills Cop','roles':[{'name':'Axel Foley','actor':'Eddie Murphy'},{'name':'Axel Foley','actor':'Eddie Murphy'}]}";
        #endregion

        [TestMethod]
        public void CreateMovieObject()
        {
            var movie = new Movie();
            Assert.IsNotNull(movie);
        }

        [TestMethod]
        public void ReadAndWriteMovieProperties()
        {
            var movie = new Movie
            {
                Name = "Star Trek"
            };

            Assert.AreEqual("Star Trek", movie.Name);
            Assert.IsFalse(movie.HasRoles);
        }

        [TestMethod]
        public void LoadMovieFromJson()
        {
            var movie = JsonConvert.DeserializeObject<Movie>(VALID_MOVIE);

            Assert.AreEqual("Beverly Hills Cop", movie.Name);
            Assert.IsTrue(movie.HasRoles);
            Assert.AreEqual(1, movie.Roles.Count);
        }

        [TestMethod]
        public void LoadMovieWithNullNameFromJson()
        {
            var movie = JsonConvert.DeserializeObject<Movie>(MOVIE_NULL_NAME);

            Assert.AreEqual("Missing", movie.Name);
            Assert.IsTrue(movie.HasRoles);
            Assert.AreEqual(1, movie.Roles.Count);
        }

        [TestMethod]
        public void LoadMovieWithEmptyNameFromJson()
        {
            var movie = JsonConvert.DeserializeObject<Movie>(MOVIE_EMPTY_NAME);

            Assert.AreEqual("Missing", movie.Name);
            Assert.IsTrue(movie.HasRoles);
            Assert.AreEqual(1, movie.Roles.Count);
        }

        [TestMethod]
        public void LoadMovieWithDuplicateRoleFromJson()
        {
            var movie = JsonConvert.DeserializeObject<Movie>(MOVIE_DUPLICATE_ROLE);

            Assert.AreEqual("Beverly Hills Cop", movie.Name);
            Assert.IsTrue(movie.HasRoles);
            Assert.AreEqual(1, movie.Roles.Count);
        }
    }
}
