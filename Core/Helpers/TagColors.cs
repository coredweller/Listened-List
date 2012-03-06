using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public static class TagColors
    {
        public static TagColor Red = new TagColor( "Red", "#FF2222" );
        public static TagColor Blue = new TagColor( "Blue", "#0033EE" );
        public static TagColor Green = new TagColor( "Green", "#339900" );
        public static TagColor Orange = new TagColor( "Orange", "#FF8811" );
        public static TagColor Yellow = new TagColor( "Yellow", "#FFDD11" );
        public static TagColor Purple = new TagColor( "Purple", "#6611BB" );
        public static TagColor Pink = new TagColor( "Pink", "#FF00AA" );
        
        public static IList<TagColor> GetAllColors() {

            var colors = new List<TagColor>();

            colors.Add( Red );
            colors.Add( Blue );
            colors.Add( Green );
            colors.Add( Orange );
            colors.Add( Yellow );
            colors.Add( Purple );
            colors.Add( Pink );

            return colors;
        }
    }
}
