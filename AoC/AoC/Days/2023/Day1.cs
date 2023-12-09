using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 1)]
    public class Day1 : IDay
    {
        public static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public string Part1(string s)
        {
            return s.Lines().Sum(line => int.Parse(new string(line.First(x => Digits.Contains(x)), 1) + new string(line.Last(x => Digits.Contains(x)), 1))).ToString();
        }

        public static readonly Dictionary<string, string> SpelledOut = new()
        {
            { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" }, { "nine", "9" },
        };

        public string Part2(string s)
        {
            return s.Lines().Select(line => line.ReplaceMany(SpelledOut.Keys.ToArray(), SpelledOut.Values.ToArray())).Sum(line => int.Parse(new string(line.First(x => Digits.Contains(x)), 1) + new string(line.Last(x => Digits.Contains(x)), 1))).ToString();
        }
    }
}
