using Movies.DomainModel;
using System.Collections.Generic;

namespace Movies.DAL
{
    public interface IMoviesContext
    {
        List<Movie> AllMovies { get; set; }
        IList<string> Actors { get; }
        ICollection<Role> RolesByActor(string actor);
    }
}
