using System.Linq;
using System.Reflection;
using Engine.Players;
using NUnit.Framework;

namespace Engine.Tests
{
    
    public class PlayerTypesTests
    {
        [Test]
        public void GetPlayerTypes_ShouldGetOneType_BecauseThereIsOnlyOneType()
        {
            // Assemble
            var assemblies = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(x => x.GetName() == "Engine")
                .ToList();


            // Act
            var factory = new PlayerFactory();

            // Assert
            Assert.AreEqual(1, assemblies.Count());
            Assert.AreEqual(1, factory.PlayerTypes.Count());
        }
    }
}
