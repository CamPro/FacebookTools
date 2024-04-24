using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reg.dcom.driver
{
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
