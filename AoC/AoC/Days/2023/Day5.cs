using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 5)]
    public class Day5 : IDay
    {
        public string Part1(string s)
        {
            var sections = s.Split("\n\n");

            var curData = sections[0].Split(": ")[1].Split(' ').Select(long.Parse).Select(n => (n, used: false));

            var mapTypes = sections[1..].Select(sect =>
            {
                var data = sect.Split("\n")[1..].Select(line => line.Split(' ').Select(long.Parse).ToArray());

                return data.Select(map => (destStart: map[0], sourceStart: map[1], rangeLen: map[2]));
            });

            foreach (var maps in mapTypes)
            {
                foreach (var (destStart, sourceStart, rangeLen) in maps)
                {
                    curData = curData.Select(data => !data.used && (data.n >= sourceStart) && (data.n < sourceStart + rangeLen) ? (data.n - sourceStart + destStart, true) : data);
                }

                curData = curData.Select(data => (data.n, false));
            }

            return curData.Select(data => data.n).Min().ToString();
        }

        public string Part2(string s)
        {
            var sections = s.Split("\n\n");

            var curData = sections[0].Split(": ")[1].Split(' ').Select(long.Parse).Select((n, i) => (n, i)).GroupBy(data => data.i / 2).Select(group => (start: group.First().n, end: group.First().n + group.Last().n, used: false)).ToList();

            var mapTypes = sections[1..].Select(sect =>
            {
                var data = sect.Split("\n")[1..].Select(line => line.Split(' ').Select(long.Parse).ToArray());

                return data.Select(map => (destStart: map[0], sourceStart: map[1], rangeLen: map[2]));
            });

            foreach (var maps in mapTypes)
            {
                foreach (var (destStart, sourceStart, rangeLen) in maps)
                {
                    var sourceEnd = sourceStart + rangeLen;

                    var extra = new List<(long start, long end, bool used)>();

                    curData = curData.Select(data =>
                    {
                        if (data.used) return data;

                        (long start, long end)? before = (data.start >= sourceStart) ? null : (data.start, end: Math.Min(data.end, sourceStart));
                        (long start, long end)? contained = (data.start >= sourceEnd || data.end <= sourceStart) ? null : (start: Math.Max(sourceStart, data.start), Math.Min(data.end, sourceEnd));
                        (long start, long end)? after = (data.end <= sourceEnd) ? null : (start: Math.Max(data.start, sourceEnd), data.end);

                        (long start, long end)? newContained = contained is not null ? (start: (contained?.start ?? 0) - sourceStart + destStart, end: (contained?.end ?? 0) - sourceStart + destStart) : null;

                        if (before is not null) extra.Add((before?.start ?? 0, before?.end ?? 0, false));
                        if (after is not null) extra.Add((after?.start ?? 0, after?.end ?? 0, false));

                        return (newContained?.start ?? 0, newContained?.end ?? 0, true);
                    }).ToList();

                    foreach (var e in extra) curData.Add(e);
                }

                curData = curData.Select(data => (data.start, data.end, false)).Where(data => data.start != 0 || data.end != 0).ToList();
            }

            return curData.Select(data => data.start).Min().ToString();
        }
    }
}
