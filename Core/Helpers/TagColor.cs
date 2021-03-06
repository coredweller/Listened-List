﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public class TagColor
    {
        public string ColorName { get; private set; }
        public string Hex { get; private set; }
        public string CssClass { get; private set; }

        public TagColor( string colorName, string hex, string cssClass ) {
            ColorName = colorName;
            Hex = hex;
            CssClass = cssClass;
        }
    }
}
