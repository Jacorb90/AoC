using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 8)]
    public class Day8 : IDay
    {
        public string Part1(string s)
        {
            var inputLines = s.Lines();
            var directions = inputLines.First().Select(lr => lr == 'R').ToArray();
            var adjacencyMatrix = inputLines.Skip(2).Select(line =>
            {
                var data = line.Split(" = ");
                return (src: data[0], targets: data[1][1..^1].Split(", "));
            }).ToDictionary(pair => pair.src, pair => pair.targets);

            var current = "AAA";
            var directionIndex = -1;
            var steps = 0;
            while (current != "ZZZ")
            {
                current = adjacencyMatrix[current][directions[directionIndex = (directionIndex + 1) % directions.Length] ? 1 : 0];
                steps++;
            }

            return steps.ToString();
        }

        public string Part2(string s)
        {
            var inputLines = s.Lines();
            var directions = inputLines.First().Select(lr => lr == 'R').ToArray();
            var adjacencyMatrix = inputLines.Skip(2).Select(line =>
            {
                var data = line.Split(" = ");
                return (src: data[0], targets: data[1][1..^1].Split(", "));
            }).ToDictionary(pair => pair.src, pair => pair.targets);

            var startingNodes = adjacencyMatrix.Keys.Where(key => key.EndsWith('A'));
            Dictionary<int, List<string>> cache = new();

            foreach (var start in startingNodes)
            {
                var current = start;
                var directionIndex = -1;
                var steps = 0;
                while (!current.EndsWith('Z') || !cache.ContainsKey(steps) || !cache[steps].Contains(start))
                {
                    current = adjacencyMatrix[current][directions[directionIndex = (directionIndex + 1) % directions.Length] ? 1 : 0];
                    steps++;

                    if (current.EndsWith('Z'))
                    {
                        if (!cache.ContainsKey(steps)) cache[steps] = new();
                        cache[steps].Add(start);
                    };
                }
            }

            return cache.Aggregate(1L, (a, kvp) => a.LCM(kvp.Key)).ToString();
        }
    }
}
