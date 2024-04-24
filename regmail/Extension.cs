using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regmail
{

     public static class StringExtension
     {
          public static string GetLast(this string source, int tail_length)
          {
               if (tail_length >= source.Length)
                    return source;
               return source.Substring(source.Length - tail_length);
          }
     }


     static public class Extension
          {

               public static List<T> RandomList<T>(this List<T> input, int take = 0)
               {
                    Random rnd = new Random();
                    var result = (from item in input
                                  orderby rnd.Next()
                                  select item).ToList();
                    return result.Take(take == 0 ? result.Count : take).ToList();

               }
          }
    
}
