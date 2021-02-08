using Engine.Players;
using Engine.ValueTypes;
using FluentAssertions;
using NUnit.Framework;

namespace Engine.Tests
{
    
    public class PlayerTypesTests
    {
        [Test]
        // TODO : No clue how to really do this dynamically.  Delete?
        public void GetPlayerTypes_ShouldGetTwoTypes_BecauseThereAreTwoTypes()
        {
            // Assemble

            // Act
            var factory = new PlayerFactory();

            // Assert
            factory.PlayerTypes.Should().HaveCount(2, "because there are two types currently");
        }

        [Test]
        public void CreatePlayerOfType_ShouldCreatePlayer_WhenWeAskForPlayer()
        {
            // Assemble
            var factory = new PlayerFactory();
            var args = new PlayerConstructorArguments(11, PlayerNumber.FirstPlayer);

            // Act
            var player = factory.CreatePlayerOfType("TestPlayer", args);

            // Assert
            player.Should().BeOfType(typeof(TestPlayer), "because that is what we chose");
        }
    }
}
