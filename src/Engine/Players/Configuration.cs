using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Engine.Players
{
    public class Configuration
    {
        public string TypeName { get; set; }
        public string PlayerName { get; set; }
        public Dictionary<string, string> Options { get; set; } 
            = new Dictionary<string, string>();
        
        public Configuration(string typeName, string playerName)
        {
            TypeName = typeName;
            PlayerName = playerName;
        }

        public static Configuration GetConfiguration(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<Configuration>(jsonData);
            return data;
        }
    }
}
