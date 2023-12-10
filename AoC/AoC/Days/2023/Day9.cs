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
        private static double Extrapolate(int index, IEnumerable<int> seq)
        {
            var withIndices = seq.Select((y, i) => (y, i));
            return withIndices.Sum(pair1 => withIndices.Select(pair2 => pair2.i == pair1.i ? pair1.y : (double)(index - pair2.i) / (pair1.i - pair2.i)).Prod());
        }

        public string Part1(string input)
        {
            return Math.Round(input.Lines().Select(line =>
            {
                var l = line.Split(' ').Select(int.Parse);
                return Extrapolate(l.Count(), l);
            }).Sum()).ToString();
        }

        public string Part2(string input)
        {
            return Math.Round(input.Lines().Select(line => Extrapolate(-1, line.Split(' ').Select(int.Parse))).Sum()).ToString();
        }
    }
}
