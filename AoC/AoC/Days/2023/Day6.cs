using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 6)]
    public class Day6 : IDay
    {
        public string Part1(string s)
        {
            var inputLines = s.Lines();
            var timeData = inputLines.First().Split(":      ")[1].Split("  ").Where(x => x.Length > 0).Select((x, i) => (i, n: int.Parse(x.Trim())));
            var distanceData = inputLines.Last().Split(":  ")[1].Split("  ").Where(x => x.Length > 0).Select((x, i) => (i, n: int.Parse(x.Trim())));

            var pairs = timeData.Join(distanceData, data => data.i, data => data.i, (outer, inner) => (time: outer.n, distance: inner.n));

            // quadratic approximation strats ftw
            var counts = pairs.Select(pair =>
            {
                var delta = Math.Sqrt(Math.Pow(pair.time, 2) - 4 * pair.distance) / 2;
                var right = pair.time / 2d + delta;
                var left = pair.time / 2d - delta;

                return Math.Ceiling(right) - Math.Floor(left) - 1;
            });

            return Math.Round(Math.Exp(counts.Sum(Math.Log))).ToString();
        }

        public string Part2(string s)
        {
            var inputLines = s.Lines();
            var time = long.Parse(new string(inputLines.First().Split(":")[1].Where(c => c != ' ').ToArray()));
            var distance = long.Parse(new string(inputLines.Last().Split(":")[1].Where(c => c != ' ').ToArray()));

            // quadratic approximation strats ftw
            var delta = Math.Sqrt(Math.Pow(time, 2) - 4 * distance) / 2;
            var right = time / 2d + delta;
            var left = time / 2d - delta;

            return (Math.Ceiling(right) - Math.Floor(left) - 1).ToString();
        }
    }
}
