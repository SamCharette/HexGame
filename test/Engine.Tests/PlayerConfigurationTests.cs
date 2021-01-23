using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Players;
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
            Assert.IsInstanceOf<Configuration>(newConfig);
            Assert.AreEqual(0, newConfig.Options.Count);
        }

        [Test]
        public void CreateFromJson_ShouldCreateObject_IfJsonIsFormattedRight()
        {
            // assemble
            var json = "{\"TypeName\" : \"Player\",\"PlayerName\" : \"Test Player\"}";

            // act
            var config = Configuration.GetConfiguration(json);

            // assert
            Assert.AreEqual(typeof(Configuration), config.GetType());
            Assert.AreEqual("Player", config.TypeName);
            Assert.AreEqual("Test Player", config.PlayerName);
            Assert.AreEqual(0, config.Options.Count);
        }

        [Test]
        public void CreateFromJson_ShouldCreateObjectWithOptions_IfJsonIsFormattedRight()
        {
            // assemble
            var json = "{\"TypeName\" : \"Player\",\"PlayerName\" : \"Test Player\",\"options\" : {\"First option\" : \"is an option\",\"Second option\" : \"is also an option\"}}";

            // act
            var config = Configuration.GetConfiguration(json);

            // assert
            Assert.AreEqual(typeof(Configuration), config.GetType());
            Assert.AreEqual("Player", config.TypeName);
            Assert.AreEqual("Test Player", config.PlayerName);
            Assert.AreEqual(2, config.Options.Count);
            Assert.AreEqual("is an option", config.Options.FirstOrDefault(x => x.Key == "First option").Value);
        }
    }
}
