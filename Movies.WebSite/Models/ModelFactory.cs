using System.Collections.Generic;
using System.Linq;

namespace Movies.WebSite.Models
{
    public class ModelFactory
    {
        public List<Actor> Create(IEnumerable<string> actors, DomainModel.Movies movies)
        {
            return actors.Select(actorName => Create(actorName, movies)).OrderBy(actor => actor.Name).ToList();
        }

        public Actor Create(string actorName, DomainModel.Movies movies)
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
