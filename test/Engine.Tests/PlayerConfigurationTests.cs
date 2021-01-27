using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Players;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Engine.Tests
{
    
    public class PlayerConfigurationTests
    {
        [Test]
        public void CreateFromJson_ShouldFail_IfJsonNotFormattedRight()
        {
            // assemble
            var json = "no json here";

            // act
            var newConfig = Configuration.GetConfiguration(json);

            // assert
            newConfig.Should().BeNull("because the json was bad");
        }

        [Test]
        public void CreateFromJson_ShouldCreateObject_IfJsonIsFormattedRight()
        {
            // assemble
            var json = "{\"TypeName\" : \"Player\",\"PlayerName\" : \"Test Player\"}";

            // act
            var config = Configuration.GetConfiguration(json);

            // assert
            config.Should().BeOfType(typeof(Configuration), "because the json was good");
            config.TypeName.Should().BeEquivalentTo("Player", "because the type given was Player");
            config.PlayerName.Should().BeEquivalentTo("Test Player", "because that is the name we gave it");
            config.Options.Should().HaveCount(0, "because no options were given");
        }

        [Test]
        public void CreateFromJson_ShouldCreateObjectWithOptions_IfJsonIsFormattedRight()
        {
            // assemble
            var json = "{\"TypeName\" : \"Player\",\"PlayerName\" : \"Test Player\",\"options\" : {\"First option\" : \"is an option\",\"Second option\" : \"is also an option\"}}";

            // act
            var config = Configuration.GetConfiguration(json);

            // assert
            config.Should().BeOfType(typeof(Configuration), "because the json was good");
            config.TypeName.Should().BeEquivalentTo("Player", "because the type given was Player");
            config.PlayerName.Should().BeEquivalentTo("Test Player", "because that is the name we gave it");
            config.Options.Should().HaveCount(2, "because 2 options were given");
            config.Options.FirstOrDefault(x => x.Key == "First option").Value.Should()
                .BeEquivalentTo("is an option", "because that is what we set it to");
        }
    }
}
