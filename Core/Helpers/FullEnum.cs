using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public class FullEnum
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string StringValue { get; set; }

        public FullEnum( string name, int value, string stringValue ) {
            Name = name;
            Value = value;
            StringValue = stringValue;
        }
    }
}
