using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.DAL;
using Movies.WebSite.Pages;
using NSubstitute;

namespace Movies.WebSite.Tests.Pages
{
    [TestClass]
    public class IndexModelTests
    {
        [TestMethod]
        public void OnGet()
        {
            var repository = Substitute.For<IMoviesRepository>();
            repository.GetAllMovies().Returns(new DAL.MoviesContext());

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet();

            Assert.IsNotNull(indexModel.Actors);
            Assert.IsFalse(indexModel.HaveActors);
            repository.Received(1).GetAllMovies();
        }

        [TestMethod]
        public void OnGetNoData()
        {
            var repository = Substitute.For<IMoviesRepository>();
            DAL.MoviesContext movies = null;
            repository.GetAllMovies().Returns(movies);

            var indexModel = new IndexModel(repository);
            Assert.IsNotNull(indexModel);
            indexModel.OnGet();

            Assert.IsNotNull(indexModel.Actors);
            Assert.IsFalse(indexModel.HaveActors);
            repository.Received(1).GetAllMovies();
        }
    }
}
