using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.ValueTypes
{
    public class Log
    {
        public DateTime DateTime { get; private set; }
        public string Text { get; private set; }

        public Log(DateTime dateTime, string text)
        {
            DateTime = dateTime;
            Text = text;
        }
    }
}
