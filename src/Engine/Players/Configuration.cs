using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Engine.Players
{
    public class Configuration
    {
        public string TypeName { get; set; }
        public string PlayerName { get; set; }
        public JObject Options { get; set; }
        
        public Configuration(string json)
        {
            Options = JObject.Parse(json);

            TypeName = GetStringForOption(nameof(TypeName));
            PlayerName = GetStringForOption(nameof(PlayerName));

        }

        public string GetStringForOption(string name)
        {
            if (!string.IsNullOrWhiteSpace(Options[name]?.ToString()))
            {
                return Options[nameof(PlayerName)]?.ToString();
            }

            return null;
        }

        public int GetIntFromOption(string name)
        {
            var tempValue = GetStringForOption(name);
            var newIntValue = 0;    

            if (!string.IsNullOrWhiteSpace(tempValue))
            {
                int.TryParse(tempValue, out newIntValue);
            }

            return newIntValue;
        }


        public static Configuration GetConfiguration(string jsonData)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<Configuration>(jsonData);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
