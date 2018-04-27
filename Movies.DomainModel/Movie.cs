using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Movies.DomainModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Movie
    {
        private string _name;
        private const string DEFAULT_MOVIE_NAME = "Missing";

        public Movie()
        {
            Roles = new HashSet<Role>();
        }

        [DefaultValue(DEFAULT_MOVIE_NAME)]
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? DEFAULT_MOVIE_NAME : value;
        }

        /// <summary>
        /// Using a HashSet so that the Roles added are unique.
        /// </summary>
        [JsonProperty("roles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public HashSet<Role> Roles { get; set; }

        public bool HasRoles => Roles.Count > 0;

        [JsonProperty("hasRoles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool GetHasRoles { get { return HasRoles; } }
    }
}
