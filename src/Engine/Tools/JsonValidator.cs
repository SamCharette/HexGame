using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Engine.Tools
{
    public class JsonValidator<T>
    {
        public bool IsValid(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return false;
            }

            var value = json.Trim();

            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(T));

            JObject newObject = JObject.Parse(value);

            return newObject.IsValid(schema);

        }
    }
}
