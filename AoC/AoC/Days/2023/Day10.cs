using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 10)]
    public class Day10 : IDay
    {
        private static readonly Dictionary<char, (int x, int y)[]> Neighbours = new()
        {
            { '|', new[]{ (0, 1), (0, -1) } },
            { '-', new[]{ (1, 0), (-1, 0) } },
            { 'L', new[]{ (0, -1), (1, 0) } },
            { 'J', new[]{ (-1, 0), (0, -1) } },
            { '7', new[]{ (-1, 0), (0, 1) } },
            { 'F', new[]{ (0, 1), (1, 0) } },
            { '.', Array.Empty<(int x, int y)>() },
            { 'S', new[]{ (-1, 0), (1, 0), (0, -1), (0, 1) } }
        };

        public string Part1(string input)
        {
            var map = input.Split('\n').SelectMany((s, y) => s.Trim().Select((c,x) => (c,x,y))).ToDictionary(tri => (tri.x, tri.y), tri => tri.c);

            var size = 0;
            (int x, int y)? prev = null;
            var current = map.Where(kvp => kvp.Value == 'S').First().Key;

            do
            {
                var next = Neighbours[map[current]].Select(dir => (x: current.x + dir.x, y: current.y + dir.y))
                        .Where(map.ContainsKey)
                        .Where(pos => prev is null ? Neighbours[map[pos]].Any(dir2 => (x: pos.x + dir2.x, y: pos.y + dir2.y) == current) : pos != prev).First();
                prev = current;
                current = next;
                size++;
            } while (map[current] != 'S');


            return (size / 2).ToString();
        }

        private static readonly Dictionary<(int x, int y), (int x, int y)> LeftDir = new()
        {
            { (0, 1), (1, 0) },
            { (0, -1), (-1, 0) },
            { (1, 0), (0, -1) },
            { (-1, 0), (0, 1)  }
        };

        private static readonly Dictionary<(int x, int y), (int x, int y)> RightDir = new()
        {
            { (0, 1), (-1, 0) },
            { (0, -1), (1, 0) },
            { (1, 0), (0, 1) },
            { (-1, 0), (0, -1)  }
        };

        public string Part2(string input)
        {
            var map = input.Split('\n').SelectMany((s, y) => s.Trim().Select((c, x) => (c, x, y))).ToDictionary(tri => (tri.x, tri.y), tri => tri.c);

            var loop = new List<(int x, int y)>();
            var outsideR = map.Keys.ToList();
            var outsideL = map.Keys.ToList();
            (int x, int y)? prev = null;
            var current = map.Where(kvp => kvp.Value == 'S').First().Key;

            do
            {
                loop.Add(current);
                if (map[current] != 'S')
                {
                    var toRights = Neighbours[map[current]].Select(dir => (x: current.x + dir.x, y: current.y + dir.y, nx: current.x + RightDir[dir].x, ny: current.y + RightDir[dir].y, rx: current.x + dir.x + RightDir[dir].x, ry: current.y + dir.y + RightDir[dir].y))
                        .Where(data => map.ContainsKey((data.x, data.y)) && map.ContainsKey((x: data.nx, y: data.ny)) && map.ContainsKey((x: data.rx, y: data.ry)))
                        .Where(data => (data.x, data.y) != prev).SelectMany(data => new[] { (x: data.nx, y: data.ny), (x: data.rx, y: data.ry) });
                    if (toRights.Any())
                    {
                        foreach (var r in toRights)
                        {
                            outsideR.Remove(r);
                        }
                    }

                    var toLefts = Neighbours[map[current]].Select(dir => (x: current.x + dir.x, y: current.y + dir.y, nx: current.x + LeftDir[dir].x, ny: current.y + LeftDir[dir].y, rx: current.x + dir.x + LeftDir[dir].x, ry: current.y + dir.y + LeftDir[dir].y))
                        .Where(data => map.ContainsKey((data.x, data.y)) && map.ContainsKey((x: data.nx, y: data.ny)) && map.ContainsKey((x: data.rx, y: data.ry)))
                        .Where(data => (data.x, data.y) != prev).SelectMany(data => new[] { (x: data.nx, y: data.ny), (x: data.rx, y: data.ry) });
                    if (toLefts.Any())
                    {
                        foreach (var r in toLefts)
                        {
                            outsideL.Remove(r);
                        }
                    }
                }
                var next = Neighbours[map[current]].Select(dir => (x: current.x + dir.x, y: current.y + dir.y))
                        .Where(map.ContainsKey)
                        .Where(pos => prev is null ? Neighbours[map[pos]].Any(dir2 => (x: pos.x + dir2.x, y: pos.y + dir2.y) == current) : pos != prev).Last();
                prev = current;
                current = next;
            } while (map[current] != 'S');

            var insideR = new List<(int x, int y)>();
            var diff = map.Keys.Where(pos => !outsideR.Contains(pos) && !loop.Contains(pos));
            do
            {
                insideR.AddRange(diff);
                diff = map.Keys.Where(pos => !insideR.Contains(pos) && !loop.Contains(pos) && insideR.Any(ins => Math.Max(Math.Abs(ins.x - pos.x), Math.Abs(ins.y - pos.y)) <= 1));
            } while (diff.Any());

            var insideL = new List<(int x, int y)>();
            diff = map.Keys.Where(pos => !insideL.Contains(pos) && !loop.Contains(pos));
            do
            {
                insideL.AddRange(diff);
                diff = map.Keys.Where(pos => !insideL.Contains(pos) && !loop.Contains(pos) && insideL.Any(ins => Math.Max(Math.Abs(ins.x - pos.x), Math.Abs(ins.y - pos.y)) <= 1));
            } while (diff.Any());

            return insideR.Count.ToString() + ", " + insideL.Count.ToString();
        }
    }
}
