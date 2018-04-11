using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Movies.DomainModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Role : IEquatable<Role>
    {
        private const string DEFAULT_CHARACTER_NAME = "Missing";
        private const string DEFAULT_ACTOR_NAME = "Unknown";
        private string _actorName;
        private string _characterName;

        [DefaultValue(DEFAULT_CHARACTER_NAME)]
        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string CharacterName
        {
            get => _characterName;
            set => _characterName = string.IsNullOrWhiteSpace(value) ? DEFAULT_CHARACTER_NAME : value;
        }

        [DefaultValue(DEFAULT_ACTOR_NAME)]
        [JsonProperty("actor", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string ActorName
        {
            get => _actorName;
            set => _actorName = string.IsNullOrWhiteSpace(value) ? DEFAULT_ACTOR_NAME : value;
        }

        public Movie Movie { get; set; }

        /// <summary>
        /// Implement IEquatable so that the object is correctly compared.
        /// </summary>
        /// <param name="other">The object to equate against.</param>
        /// <returns>If the objects are equal based on the Character Name and Actor Name</returns>
        public bool Equals(Role other)
        {
            if (other == null)
            {
                return false;
            }

            return StringComparer.Ordinal.Equals(CharacterName, other.CharacterName) &&
                   StringComparer.Ordinal.Equals(ActorName, other.ActorName);
        }

        // Need to override Equals and GetHashCode to get IEquatable to work.
        public override bool Equals(object obj)
        {
            if (!(obj is Role other))
            {
                return false;
            }
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return 1;
        }
    }
}
