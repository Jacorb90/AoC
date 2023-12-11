using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 11)]
    public class Day11 : IDay
    {
        public string Part1(string input)
        {
            var rows = input.Split('\n');
            var cols = rows.Select((row, i) => new string(rows.Select(r2 => r2[i]).ToArray()));
            var emptyRows = rows.Select((row,i) => (row,i)).Where(pair => !pair.row.Any(c => c == '#')).Select(pair => pair.i);
            var emptyCols = cols.Select((col, i) => (col, i)).Where(pair => !pair.col.Any(c => c == '#')).Select(pair => pair.i);
            var rowScores = rows.Select((_, i) => emptyRows.Contains(i) ? 2 : 1).ToArray();
            var colScores = cols.Select((_, i) => emptyCols.Contains(i) ? 2 : 1).ToArray();
            var galaxies = rows.SelectMany((s, y) => s.Trim().Select((c, x) => (c, x, y))).Where(tri => tri.c == '#').Select(tri => (tri.x, tri.y)).ToList();

            var totalDist = 0;
            for (int gA=0; gA<galaxies.Count; gA++)
            {
                var g1 = galaxies[gA];
                for (int gB=0; gB<gA; gB++)
                {
                    var old = totalDist;
                    var g2 = galaxies[gB];
                    if (g1.x <= g2.x)
                    {
                        for (var x=g1.x + 1; x<=g2.x; x++)
                        {
                            totalDist += colScores[x];
                        }
                    } else
                    {
                        for (var x = g2.x + 1; x <= g1.x; x++)
                        {
                            totalDist += colScores[x];
                        }
                    }

                    if (g1.y <= g2.y)
                    {
                        for (var y = g1.y + 1; y <= g2.y; y++)
                        {
                            totalDist += rowScores[y];
                        }
                    }
                    else
                    {
                        for (var y = g2.y + 1; y <= g1.y; y++)
                        {
                            totalDist += rowScores[y];
                        }
                    }
                }
            }

            return totalDist.ToString();
        }

        public string Part2(string input)
        {
            var rows = input.Split('\n');
            var cols = rows.Select((row, i) => new string(rows.Select(r2 => r2[i]).ToArray()));
            var emptyRows = rows.Select((row, i) => (row, i)).Where(pair => !pair.row.Any(c => c == '#')).Select(pair => pair.i);
            var emptyCols = cols.Select((col, i) => (col, i)).Where(pair => !pair.col.Any(c => c == '#')).Select(pair => pair.i);
            var rowScores = rows.Select((_, i) => emptyRows.Contains(i) ? 1000000 : 1).ToArray();
            var colScores = cols.Select((_, i) => emptyCols.Contains(i) ? 1000000 : 1).ToArray();
            var galaxies = rows.SelectMany((s, y) => s.Trim().Select((c, x) => (c, x, y))).Where(tri => tri.c == '#').Select(tri => (tri.x, tri.y)).ToList();

            var totalDist = 0L;
            for (int gA = 0; gA < galaxies.Count; gA++)
            {
                var g1 = galaxies[gA];
                for (int gB = 0; gB < gA; gB++)
                {
                    var old = totalDist;
                    var g2 = galaxies[gB];
                    if (g1.x <= g2.x)
                    {
                        for (var x = g1.x + 1; x <= g2.x; x++)
                        {
                            totalDist += colScores[x];
                        }
                    }
                    else
                    {
                        for (var x = g2.x + 1; x <= g1.x; x++)
                        {
                            totalDist += colScores[x];
                        }
                    }

                    if (g1.y <= g2.y)
                    {
                        for (var y = g1.y + 1; y <= g2.y; y++)
                        {
                            totalDist += rowScores[y];
                        }
                    }
                    else
                    {
                        for (var y = g2.y + 1; y <= g1.y; y++)
                        {
                            totalDist += rowScores[y];
                        }
                    }
                }
            }

            return totalDist.ToString();
        }
    }
}
