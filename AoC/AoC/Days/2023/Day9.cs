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
            return withIndices.Sum(pair1 => withIndices.Prod(pair2 => pair2.i == pair1.i ? pair1.y : (index - pair2.i)) / withIndices.Prod(pair2 => pair2.i == pair1.i ? 1d : (pair1.i - pair2.i)));
        }

        public string Part1(string input)
        {
            return ((long)input.Lines().Select(line =>
            {
                var l = line.Split(' ').Select(int.Parse);
                return Extrapolate(l.Count(), l);
            }).Sum()).ToString();
        }

        public string Part2(string input)
        {
            return ((long)input.Lines().Select(line =>
            {
                var l = line.Split(' ').Select(int.Parse);
                return Extrapolate(-1, l);
            }).Sum()).ToString();
        }
    }
}
