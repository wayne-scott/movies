using Movies.DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace Movies.DAL
{
    public class MoviesContext : IMoviesContext
    {
        private List<string> _actors;

        public MoviesContext()
        {
            AllMovies = new List<Movie>();
        }

        public List<Movie> AllMovies { get; set; }

        public IList<string> Actors => _actors ?? (_actors = GetUniqueListOfActors());

        private List<string> GetUniqueListOfActors()
        {
            var actors = new List<string>();
            foreach (var movie in AllMovies)
            {
                actors.AddRange(movie.Roles.Select(role => role.ActorName).Distinct().Where(s => !actors.Contains(s)));
            }

            return actors;
        }

        public void Initialise(List<Movie> listOfMovies)
        {
            // Add a link to the movie for each role to make object navigation easier.
            foreach (var movie in listOfMovies)
            {
                movie.Roles.ToList().ForEach(role => role.Movie = movie);
            }
            AllMovies = listOfMovies;
        }

        public ICollection<Role> RolesByActor(string actor)
        {
            var roles = new List<Role>();
            foreach (var movie in AllMovies)
            {
                roles.AddRange(movie.Roles.Where(role => role.ActorName == actor));
            }

            return roles;
        }
    }
}
