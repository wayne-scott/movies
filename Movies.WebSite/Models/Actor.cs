using System.Collections.Generic;
using Movies.DomainModel;

namespace Movies.WebSite.Models
{
    public class Actor
    {
        public Actor() => Roles = new List<Role>();

        public bool HasRoles => Roles.Count > 0;

        public IList<Role> Roles { get; set; }

        public string Name { get; set; }
    }
}
