using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Engine.Players
{
    public class PlayerFactory
    {
        public List<PlayerType> PlayerTypes { get; private set; }
        private Assembly Assembly { get; set; }
        public PlayerFactory(Assembly assembly = null)
        {
            Assembly = assembly ?? Assembly.GetExecutingAssembly();
            PlayerTypes = GetPlayerTypes();
        }
        private List<PlayerType> GetPlayerTypes()
        {
            var pType = typeof(BasePlayer);

            return  Assembly
                    .GetTypes()
                    .Where(t => 
                        t.IsClass 
                        && t != pType
                        && pType.IsAssignableFrom(t))
                    .Select(t => new PlayerType( t.Name, t))
                    .ToList();
        }

        public BasePlayer CreatePlayerOfType(string playerType, PlayerConstructorArguments args)
        {
            if (PlayerTypes.Any(x => x.Name == playerType))
            {
                var type = PlayerTypes.FirstOrDefault(x => x.Name == playerType)?.Type;
                if (type == null)
                {
                    return null;
                }
                dynamic player = (BasePlayer)Activator.CreateInstance(type, args);
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
