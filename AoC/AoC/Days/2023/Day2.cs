using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 2)]
    public class Day2 : IDay
    {
        public enum RGB
        {
            Red, Green, Blue
        }

        private static readonly Dictionary<string, RGB> Colours = new()
        {
            { "red", RGB.Red }, { "green", RGB.Green }, { "blue", RGB.Blue }
        };

        private static readonly Dictionary<RGB, int> MaxCounts = new()
        {
            { RGB.Red, 12 }, { RGB.Green, 13 }, { RGB.Blue, 14 }
        };

        public string Part1(string s)
        {
            return s.Lines().Sum(l =>
            {
                var spl = l.Split(": ");
                var game = int.Parse(spl[0].Split(" ")[^1]);
                var rounds = spl[1].Split("; ");
                var possible = rounds.All(round =>
                {
                    var items = round.Split(", ");
                    var counts = new Dictionary<RGB, int>(MaxCounts);

                    foreach (var item in items)
                    {
                        var pair = item.Split(" ");
                        counts[Colours[pair[^1]]] -= int.Parse(pair[0]);
                    }

                    return counts.Values.All(v => v >= 0);
                });
                return possible ? game : 0;
            }).ToString();
        }

        private static readonly Dictionary<RGB, int> DictZero = new() { { RGB.Red, 0 }, { RGB.Green, 0 }, { RGB.Blue, 0 } };

        public string Part2(string s)
        {
            return s.Lines().Sum(l =>
            {
                var spl = l.Split(": ");
                var game = int.Parse(spl[0].Split(" ")[^1]);
                var rounds = spl[1].Split("; ");
                var limits = rounds.Select(round =>
                {
                    var items = round.Split(", ");
                    var counts = new Dictionary<RGB, int>(DictZero);

                    foreach (var item in items)
                    {
                        var pair = item.Split(" ");
                        counts[Colours[pair[^1]]] += int.Parse(pair[0]);
                    }

                    return counts;
                }).Aggregate(new Dictionary<RGB, int>(DictZero), (a, c) =>
                {
                    a[RGB.Red] = Math.Max(a[RGB.Red], c[RGB.Red]);
                    a[RGB.Green] = Math.Max(a[RGB.Green], c[RGB.Green]);
                    a[RGB.Blue] = Math.Max(a[RGB.Blue], c[RGB.Blue]);
                    return a;
                });
                return limits[RGB.Red] * limits[RGB.Green] * limits[RGB.Blue];
            }).ToString();
        }
    }
}
