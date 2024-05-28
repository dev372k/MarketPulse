using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Helpers
{
    public static class ConversionHelper
    {
        public static string toCSV(this List<string> values)
        {
            return string.Join(",", values);
        }
        
        public static List<string> toStringList(this string value)
        {
            return new List<string>(value.Split(','));
        }
        public static List<int> toIntList(this string value)
        {
            return value.Split(',')
               .Select(int.Parse)
               .ToList();
        }


        public static List<int> FlattenAndParseToInt(List<List<string>> listOfLists)
        {
            return listOfLists
                .SelectMany(innerList => innerList) // Flatten the list of lists
                .Select(str => int.Parse(str)) // Parse each string to integer
                .ToList();
        }
        public static List<string> FlattenListOfLists(List<List<string>> listOfLists)
        {
            return listOfLists.SelectMany(list => list).ToList();
        }

        public static T ConvertTo<T>(dynamic value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting value '{value}' to type {typeof(T)}: {ex.Message}");
                return default(T);
            }
        }
    }
}
