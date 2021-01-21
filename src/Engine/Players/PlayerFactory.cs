using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Engine.Players
{
    public class PlayerFactory
    {
        public List<PlayerType> PlayerTypes { get; private set; }
        public Assembly Assembly { get; private set; }
        public PlayerFactory(Assembly assembly = null)
        {
            Assembly = assembly ?? Assembly.GetExecutingAssembly();
            PlayerTypes = GetPlayerTypes();
        }
        private List<PlayerType> GetPlayerTypes()
        {
            var pType = typeof(IPlayer);

            return  Assembly
                    .GetTypes()
                    .Where(t => 
                        t.IsClass 
                        && t != pType
                        && pType.IsAssignableFrom(t))
                    .Select(t => new PlayerType( t.Name, t))
                    .ToList();
        }

        public PlayerBase<PlayerConstructorArguments> CreatePlayerOfType(string playerType, PlayerConstructorArguments args)
        {
            if (PlayerTypes.Any(x => x.Name == playerType))
            {
                var type = PlayerTypes.FirstOrDefault(x => x.Name == playerType).Type;
                dynamic player = (PlayerBase<PlayerConstructorArguments>)Activator.CreateInstance(type, args);
                return player;
            }

            return null;
        }

        public class PlayerType
        {
            public string Name { get; private set; }
            public Type Type { get; private set; }
            public PlayerType(string name, Type type)
            {
                Type = type;
                Name = name;
            }
        }

    }


}
