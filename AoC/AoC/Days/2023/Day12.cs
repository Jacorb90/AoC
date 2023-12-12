using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 12)]
    public class Day12 : IDay
    {
        public string Part1(string input)
        {
            var rows = input.Split('\n').Select(row => row.Split(' ')).Select(arr => (data: arr[0], sizes: arr[1].Replace(","," 0 ").Split(" ").Select(int.Parse).ToList())).Select(pair =>
            {
                var actual = new List<int>();
                char? section = null;

                foreach (var c in pair.data)
                {
                    if (c != section)
                    {
                        section = c;
                        actual.Add(c == '#' ? 1 : (c == '.' ? 0 : -1));
                    } else
                    {
                        if (section == '#') actual[^1]++;
                        else if (section == '?') actual[^1]--;
                    }
                }

                if (actual[0] == 0) actual.RemoveAt(0);
                if (actual[^1] == 0) actual.RemoveAt(actual.Count - 1);

                int i = 0;
                List<int> toRemove = new();
                while (i < pair.sizes.Count && actual[i] >= 0)
                {
                    if (actual[i] == pair.sizes[i])
                    {
                        toRemove.Add(i);
                    }
                    i++;
                }
                i = 1;
                while (i <= pair.sizes.Count && actual[^i] >= 0)
                {
                    if (actual[^i] == pair.sizes[^i])
                    {
                        toRemove.Add(-i);
                    }
                    i++;
                }

                var removedLeft = 0;
                var removedRight = 0;
                foreach (var tr in toRemove)
                {
                    if (tr >= 0)
                    {
                        pair.sizes.RemoveAt(tr - removedLeft);
                        actual.RemoveAt(tr - removedLeft);
                        removedLeft++;
                    } else
                    {
                        pair.sizes.RemoveAt(pair.sizes.Count + tr + removedRight);
                        actual.RemoveAt(actual.Count + tr + removedRight);
                        removedRight++;
                    }
                }

                if (actual.Count == 1)
                {
                    if (actual[0] >= 0) return 1;
                    return -actual[0] - pair.sizes.Sum(n => Math.Max(n, 1)) + 1;
                }

                Console.WriteLine(String.Join(", ", actual));
                Console.WriteLine(String.Join(", ", pair.sizes));

                

                return -1;
            });

            return rows.Sum().ToString();
        }

        public string Part2(string input)
        {
            throw new NotImplementedException();
        }
    }
}
