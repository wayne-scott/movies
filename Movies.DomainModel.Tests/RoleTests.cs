using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Movies.DomainModel.Tests
{
    [TestClass]
    public class RoleTests
    {
        #region Test Data
        private const string VALID_ROLE = "{'name':'Axel Foley','actor':'Eddie Murphy'}";
        private const string ROLE_NULL_NAME = "{'actor':'Eddie Murphy'}";
        private const string ROLE_EMPTY_NAME = "{'name':'','actor':'Eddie Murphy'}";
        private const string ROLE_NULL_ACTOR = "{'name':'Axel Foley'}";
        private const string ROLE_EMPTY_ACTOR = "{'name':'Axel Foley','actor':''}";
        #endregion

        [TestMethod]
        public void CreateRoleObject()
        {
            var role = new Role();
            Assert.IsNotNull(role);
        }

        [TestMethod]
        public void ReadAndWriteRoleProperties()
        {
            var role = new Role
            {
                CharacterName = "Ace Merrill",
                ActorName = "Keifer Sutherland"
            };

            Assert.AreEqual("Ace Merrill", role.CharacterName);
            Assert.AreEqual("Keifer Sutherland", role.ActorName);
        }

        [TestMethod]
        public void AddMovieToRole()
        {
            var movie = new Movie();
            var role = new Role {Movie = movie};
            Assert.AreSame(movie, role.Movie);
        }

        [TestMethod]
        public void CompareRolesWithSameDetailsUsingObject()
        {
            object other = new Role {ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill"};
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsTrue(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentActorNamesUsingObject()
        {
            object other = new Role { ActorName = "Leonard Nimoy", CharacterName = "Ace Merrill" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentCharacterNamesUsingObject()
        {
            object other = new Role { ActorName = "Keifer Sutherland", CharacterName = "Spock" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentDetailsUsingObject()
        {
            object other = new Role { ActorName = "Leonard Nimoy", CharacterName = "Spock" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRoleWithDifferentTypeUsingObject()
        {
            object other = new Movie();
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithSameDetailsUsingType()
        {
            var other = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsTrue(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentActorNamesUsingType()
        {
            var other = new Role { ActorName = "Leonard Nimoy", CharacterName = "Ace Merrill" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentCharacterNamesUsingType()
        {
            var other = new Role { ActorName = "Keifer Sutherland", CharacterName = "Spock" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRolesWithDifferentDetailsUsingType()
        {
            var other = new Role { ActorName = "Leonard Nimoy", CharacterName = "Spock" };
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void CompareRoleWithNullUsingType()
        {
            Role other = null;
            var role = new Role { ActorName = "Keifer Sutherland", CharacterName = "Ace Merrill" };
            Assert.IsFalse(role.Equals(other));
        }

        [TestMethod]
        public void GetHashCodeForRole()
        {
            var role = new Role();
            Assert.AreEqual(1, role.GetHashCode());
        }

        [TestMethod]
        public void LoadRoleFromJson()
        {
            var role = JsonConvert.DeserializeObject<Role>(VALID_ROLE);

            Assert.AreEqual("Axel Foley", role.CharacterName);
            Assert.AreEqual("Eddie Murphy", role.ActorName);
        }

        [TestMethod]
        public void LoadRoleWithNullNameFromJson()
        {
            var role = JsonConvert.DeserializeObject<Role>(ROLE_NULL_NAME);

            Assert.AreEqual("Missing", role.CharacterName);
            Assert.AreEqual("Eddie Murphy", role.ActorName);
        }

        [TestMethod]
        public void LoadRoleWithEmptyNameFromJson()
        {
            var role = JsonConvert.DeserializeObject<Role>(ROLE_EMPTY_NAME);

            Assert.AreEqual("Missing", role.CharacterName);
            Assert.AreEqual("Eddie Murphy", role.ActorName);
        }

        [TestMethod]
        public void LoadRoleWithNullActorFromJson()
        {
            var role = JsonConvert.DeserializeObject<Role>(ROLE_NULL_ACTOR);

            Assert.AreEqual("Axel Foley", role.CharacterName);
            Assert.AreEqual("Unknown", role.ActorName);
        }

        [TestMethod]
        public void LoadRoleWithEmptyActorFromJson()
        {
            var role = JsonConvert.DeserializeObject<Role>(ROLE_EMPTY_ACTOR);

            Assert.AreEqual("Axel Foley", role.CharacterName);
            Assert.AreEqual("Unknown", role.ActorName);
        }
    }
}
