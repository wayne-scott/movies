using Movies.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Movies.WebSite.Models
{
    public class ModelFactory
    {
        public List<Actor> Create(IEnumerable<string> actors, IMoviesContext movies)
        {
            return actors.Select(actorName => Create(actorName, movies)).OrderBy(actor => actor.Name).ToList();
        }

        public Actor Create(string actorName, IMoviesContext movies)
        {
            if (string.IsNullOrEmpty(actorName))
            {
                return null;
            }
            return new Actor
            {
                Name = actorName,
                Roles = movies.RolesByActor(actorName).OrderBy(role => role.Movie.Name).ToList()
            };
        }
    }
}
