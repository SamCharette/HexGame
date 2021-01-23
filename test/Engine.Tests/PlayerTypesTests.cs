using System.Linq;
using System.Reflection;
using Engine.Players;
using Engine.ValueTypes;
using NUnit.Framework;

namespace Engine.Tests
{
    
    public class PlayerTypesTests
    {
        [Test]
        // TODO : No clue how to really do this dynamically.  Delete?
        public void GetPlayerTypes_ShouldGetThreeTypes_BecauseThereAreThreeTypes()
        {
            // Assemble

            // Act
            var factory = new PlayerFactory();

            // Assert
            Assert.AreEqual(3, factory.PlayerTypes.Count());
        }

        [Test]
        public void CreatePlayerOfType_ShouldCreatePlayer_WhenWeAskForPlayer()
        {
            // Assemble
            var factory = new PlayerFactory();
            var args = new PlayerConstructorArguments();
            args.BoardSize = 11;
            args.PlayerNumber = PlayerNumber.FirstPlayer;

            // Act
            var player = factory.CreatePlayerOfType("TestPlayer", args);

            // Assert
            Assert.AreEqual(player.GetType(), typeof(TestPlayer));
        }
    }
}
