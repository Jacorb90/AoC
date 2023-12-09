using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 9)]
    public class Day9 : IDay
    {
        public string Part1(string input)
        {
            var histories = input.Lines().Select(line =>
            {
                var diffs = new List<List<int>>() { line.Split(' ').Select(int.Parse).ToList() };

                while (!diffs.Last().All(d => d == 0))
                {
                    diffs.Add(diffs.Last().Skip(1).Select((d, i) => d - diffs.Last()[i]).ToList());
                }

                diffs.Last().Add(0);

                for (int i=diffs.Count-2; i>=0; i--)
                {
                    diffs[i].Add(diffs[i].Last() + diffs[i + 1].Last());
                }

                return diffs.First().Last();
            });

            return histories.Sum().ToString();
        }

        public string Part2(string input)
        {
            var histories = input.Lines().Select(line =>
            {
                var diffs = new List<List<int>>() { line.Split(' ').Select(int.Parse).ToList() };

                while (!diffs.Last().All(d => d == 0))
                {
                    diffs.Add(diffs.Last().Skip(1).Select((d, i) => d - diffs.Last()[i]).ToList());
                }

                foreach (var d in diffs)
                {
                    d.Reverse();
                }

                diffs.Last().Add(0);

                for (int i = diffs.Count - 2; i >= 0; i--)
                {
                    diffs[i].Add(diffs[i].Last() - diffs[i + 1].Last());
                }

                return diffs.First().Last();
            });

            return histories.Sum().ToString();
        }
    }
}
