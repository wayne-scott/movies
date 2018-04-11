using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movies.DomainModel;
using Movies.WebSite.Models;

namespace Movies.WebSite.Tests.Models
{
    [TestClass]
    public class ActorTests
    {
        [TestMethod]
        public void CreateActorObject()
        {
            var actor = new Actor();
            Assert.IsNotNull(actor);
        }

        [TestMethod]
        public void ReadAndWriteActorProperties()
        {
            var actor = new Actor();
            Assert.IsFalse(actor.HasRoles);
            Assert.AreEqual(0, actor.Roles.Count);
            Assert.IsTrue(string.IsNullOrEmpty(actor.Name));

            actor.Name = "Eddie Murphy";
            actor.Roles.Add(new Role());
            Assert.IsTrue(actor.HasRoles);
            Assert.AreEqual(1, actor.Roles.Count);
            Assert.AreEqual("Eddie Murphy", actor.Name);
        }
    }
}
