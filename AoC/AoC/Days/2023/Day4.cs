using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 4)]
    public class Day4 : IDay
    {
        public string Part1(string s)
        {
            var score = 0;
            foreach (var line in s.Lines())
            {
                var sides = line.Replace(":  ", ": ").Split(": ")[1].Replace("|  ", "| ").Split(" | ")
                    .Select(side => side.Replace("  ", " ").Split(' ').Select(int.Parse));

                var matches = sides.Last().Count(n => sides.First().Contains(n));
                score += (matches == 0) ? 0 : (int)Math.Pow(2, matches - 1);
            }

            return score.ToString();
        }

        public string Part2(string s)
        {
            var totalCopies = 0;
            var extraCopies = new Dictionary<int, int>();
            foreach (var line in s.Lines())
            {
                var d = line.Replace(":  ", ": ").Split(": ");
                var cardNum = int.Parse(d[0].Replace("   ", " ").Replace("  ", " ").Split(" ")[1]);
                var copies = 1 + (extraCopies.TryGetValue(cardNum, out int value) ? value : 0);
                var sides = d[1].Replace("|  ", "| ").Split(" | ")
                    .Select(side => side.Replace("  ", " ").Split(' ').Select(int.Parse));

                var matches = sides.Last().Count(n => sides.First().Contains(n));

                for (int p = 1; p <= matches; p++)
                {
                    extraCopies[cardNum + p] = (extraCopies.TryGetValue(cardNum + p, out int v) ? v : 0) + copies;
                }

                totalCopies += copies;
                extraCopies.Remove(cardNum);
            }

            return totalCopies.ToString();
        }
    }
}
