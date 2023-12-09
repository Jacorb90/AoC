using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 3)]
    public class Day3 : IDay
    {
        public string Part1(string s)
        {
            var symbolLocs = new List<(int X, int Y)>();
            var numLocs = new List<(int N, List<(int X, int Y)> Locs)>();

            var inputLineArr = s.Lines().ToArray();

            for (int y = 0; y < inputLineArr.Length; y++)
            {
                var line = inputLineArr[y];
                bool parsing = false;
                string cur = "";

                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];

                    if (Day1.Digits.Contains(c))
                    {
                        if (parsing) cur += c;
                        else
                        {
                            cur = c.ToString();
                            parsing = true;
                        }
                    }
                    else if (parsing)
                    {
                        for (int nx = x - 1; nx >= x - cur.Length; nx--)
                        {
                            if (nx == x - 1) numLocs.Add((int.Parse(cur), new List<(int X, int Y)>() { (nx, y) }));
                            else numLocs[^1].Locs.Add((nx, y));
                        }
                        parsing = false;
                        x--;
                    }
                    else if (c != '.' && c != ' ')
                    {
                        symbolLocs.Add((x, y));
                    }
                }

                if (parsing)
                {
                    for (int nx = line.Length - 1; nx >= line.Length - cur.Length; nx--)
                    {
                        if (nx == line.Length - 1) numLocs.Add((int.Parse(cur), new List<(int X, int Y)>() { (nx, y) }));
                        else numLocs[^1].Locs.Add((nx, y));
                    }
                }
            }

            var partNums = numLocs.Where(data => data.Locs.Any(pos => symbolLocs.Any(sym => Math.Max(Math.Abs(pos.X - sym.X), Math.Abs(pos.Y - sym.Y)) < 2)));

            return partNums.Sum(data => data.N).ToString();
        }

        public string Part2(string s)
        {
            var symbolLocs = new List<(int X, int Y)>();
            var numLocs = new List<(int N, List<(int X, int Y)> Locs)>();

            var inputLineArr = s.Lines().ToArray();

            for (int y = 0; y < inputLineArr.Length; y++)
            {
                var line = inputLineArr[y];
                bool parsing = false;
                string cur = "";

                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];

                    if (Day1.Digits.Contains(c))
                    {
                        if (parsing) cur += c;
                        else
                        {
                            cur = c.ToString();
                            parsing = true;
                        }
                    }
                    else if (parsing)
                    {
                        for (int nx = x - 1; nx >= x - cur.Length; nx--)
                        {
                            if (nx == x - 1) numLocs.Add((int.Parse(cur), new List<(int X, int Y)>() { (nx, y) }));
                            else numLocs[^1].Locs.Add((nx, y));
                        }
                        parsing = false;
                        x--;
                    }
                    else if (c == '*')
                    {
                        symbolLocs.Add((x, y));
                    }
                }

                if (parsing)
                {
                    for (int nx = line.Length - 1; nx >= line.Length - cur.Length; nx--)
                    {
                        if (nx == line.Length - 1) numLocs.Add((int.Parse(cur), new List<(int X, int Y)>() { (nx, y) }));
                        else numLocs[^1].Locs.Add((nx, y));
                    }
                }
            }

            var gears = symbolLocs.Where(sym => numLocs.Count(data => data.Locs.Any(pos => Math.Max(Math.Abs(pos.X - sym.X), Math.Abs(pos.Y - sym.Y)) < 2)) == 2);
            var gearRatios = gears.Select(sym => Math.Exp(numLocs.Where(data => data.Locs.Any(pos => Math.Max(Math.Abs(pos.X - sym.X), Math.Abs(pos.Y - sym.Y)) < 2)).Select(data => Math.Log(data.N)).Sum()));

            return Math.Round(gearRatios.Sum()).ToString();
        }
    }
}
