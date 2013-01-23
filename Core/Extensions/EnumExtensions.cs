using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Core.Helpers;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        public static string StringValue(this Enum value) {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static IList<string> StringValues(this Enum value) {
            Type type = value.GetType();
            IList<string> values = new List<string>();
            //Look for our string value associated with fields in this enum
            foreach (FieldInfo fi in type.GetFields()) {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if ( attrs.Length > 0 ) {
                    values.Add( attrs[0].StringValue );
                }

            }
            return values;
        }
        
        public static IDictionary<string, int> StringValuesAndEnumInts( this Enum value ) {
            
            IDictionary<string, int> ret = new Dictionary<string, int>();

            Type type = value.GetType();
            IDictionary<string, string> valuesText = new Dictionary<string, string>();
            //Look for our string value associated with fields in this enum
            foreach ( FieldInfo fi in type.GetFields() ) {
                //Check for our custom attribute
                StringValueAttribute[] attrs = fi.GetCustomAttributes( typeof( StringValueAttribute ), false ) as StringValueAttribute[];
                if ( attrs.Length > 0 ) {
                    valuesText.Add( attrs[0].StringValue, fi.Name );
                }

            }

            IDictionary<string, int> valuesInt = new Dictionary<string, int>();
            foreach ( var ttt in Enum.GetValues( type ) ) {
                valuesInt.Add( ttt.ToString(), ( (int)ttt ) );
                
            }

            var items = valuesText.Join( valuesInt,
                                    ( x ) => { return x.Value; },
                                    ( x ) => { return x.Key; },
                                    (outer, inner) =>{ return new KeyValuePair<string,int>(outer.Key,inner.Value);}

                );
            foreach(var item in items){
                ret.Add(item);
            }

            return ret;
        }

        public static IList<Tuple<string, int, string>> ParseEnum(Type type, Func<object, string> func)
        {
            var list = new List<Tuple<string, int, string>>();

            var jamTypeNames = Enum.GetNames(type);
            var jamTypeValue = Enum.GetValues(type);
            
            for (int i = 0; i < jamTypeValue.Length; i++)
            {
                var jam = jamTypeNames[i];
                var jamValue = (int)jamTypeValue.GetValue(i);
                var o = func( jamValue );
                
                list.Add( new Tuple<string, int, string>( jam, jamValue, o ) );
            }

            return list;
        }

        public static IList<FullEnum> ParseEnum(this Enum type, Func<int, string> func)
        {
            var list = new List<FullEnum>();

            var jamTypeNames = Enum.GetNames(type.GetType());
            var jamTypeValue = Enum.GetValues(type.GetType());
            
            for (int i = 0; i < jamTypeValue.Length; i++)
            {
                var jamValue = (int)jamTypeValue.GetValue(i);
                list.Add( new FullEnum( jamTypeNames[i], jamValue, func( jamValue ) ) );
            }

            return list;
        }
    }
}
