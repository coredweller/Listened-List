using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Helpers
{
    public static class TagColors
    {
        public static TagColor Red = new TagColor( "Red", "#fE1A00", "redTag" );
        public static TagColor Blue = new TagColor( "Blue", "#79bbff", "blueTag" );
        public static TagColor Green = new TagColor( "Green", "#77D42A", "greenTag" );
        public static TagColor Orange = new TagColor( "Orange", "#FFC477", "orangeTag" );
        public static TagColor Yellow = new TagColor( "Yellow", "#FFEC64", "yellowTag" );
        public static TagColor Purple = new TagColor( "Purple", "#C123DE", "purpleTag" );
        
        public static IList<TagColor> GetAllColors() {

            var colors = new List<TagColor>();

            colors.Add( Blue );
            colors.Add( Red );
            colors.Add( Green );
            colors.Add( Orange );
            colors.Add( Yellow );
            colors.Add( Purple );

            return colors;
        }

        public static TagColor GetColorByCssClass( string cssClass ) {
            return GetAllColors().SingleOrDefault( x => x.CssClass.ToLower() == cssClass.ToLower() );
        }

        public static TagColor GetColorByHex( string hex ) {
            return GetAllColors().SingleOrDefault( x => x.Hex.ToLower() == hex.ToLower() );
        }
    }
}
