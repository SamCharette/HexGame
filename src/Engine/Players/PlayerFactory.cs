using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
            var iterations = 5;
            var pType = typeof(Player);
            return Enumerable.Range(1, iterations)
                .SelectMany(i => Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsClass && t != pType
                                          && pType.IsAssignableFrom(t))
                    .Select(t => new PlayerType( t.Name, t.ReflectedType))).ToList();
        }

        public static Player CreatePlayerOfType(string playerType)
        {
            throw new NotImplementedException();
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
