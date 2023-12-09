using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Util
{
    public static class Ext
    {
        public static IEnumerable<string> Lines(this string source) => source.Split('\n');

        public static string ReplaceMany(this string source, string[] keys, string[] values)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                source = source.Replace(keys[i], keys[i][..^1] + values[i] + keys[i][1..]);
            }

            return source;
        }

        public static int ToInt(this char c)
        {
            return Convert.ToInt32(c);
        }

        public static Dictionary<T, S> DictWhere<T, S>(this Dictionary<T, S> source, Func<KeyValuePair<T, S>, bool> condition) where T : notnull
        {
            return source.Where(condition).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static long GCD(this long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public static long LCM(this long a, long b)
        {
            return (a * b) / a.GCD(b);
        }
    }
}
