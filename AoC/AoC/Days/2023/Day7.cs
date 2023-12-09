using AoC.Runner;
using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days._2023
{
    [Day(2023, 7)]
    public class Day7 : IDay
    {
        private static readonly Dictionary<char, int> CardMap = new()
        {
            { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };

        public string Part1(string s)
        {
            var inputData = s.Lines().Select(line => line.Split(' '));
            var data = inputData.Select(sects => (hand: sects[0].Select(c => CardMap.TryGetValue(c, out int v) ? v : int.Parse(c.ToString())), bid: int.Parse(sects[1])));
            var ordered = data.OrderBy(set =>
            {
                var handCode = set.hand.Aggregate(0, (a, c) => 16 * a + c);

                var handType = 0;
                var handArr = set.hand.Order().ToArray();
                for (int i = 0; i < handArr.Length; i++)
                {
                    if (i > 0)
                    {
                        if (handArr[i] == handArr[i - 1])
                        {
                            handType += (i > 1 && handArr[i] == handArr[i - 2]) ? 2 : 1;
                        }
                    }
                }

                return handType * (int)Math.Pow(16, 5) + handCode;
            }).Select((set, i) => (index: i, set.bid));

            return ordered.Sum(set => (set.index + 1) * set.bid).ToString();
        }

        private static readonly Dictionary<char, int> Part2CardMap = new()
        {
            { 'T', 10 }, { 'J', 1 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };

        public string Part2(string s)
        {
            var inputData = s.Lines().Select(line => line.Split(' '));
            var data = inputData.Select(sects => (hand: sects[0].Select(c => Part2CardMap.TryGetValue(c, out int v) ? v : int.Parse(c.ToString())), bid: int.Parse(sects[1])));
            var ordered = data.OrderBy(set =>
            {
                var handCode = set.hand.Aggregate(0, (a, c) => 16 * a + c);

                var handType = 0;
                var handArr = set.hand.Order().ToArray();
                var jokers = 0;
                for (int i = 0; i < handArr.Length; i++)
                {
                    if (handArr[i] == 1)
                    {
                        jokers++;
                    }
                    else if (i > 0)
                    {
                        if (handArr[i] == handArr[i - 1])
                        {
                            handType += (i > 1 && handArr[i] == handArr[i - 2]) ? 2 : 1;
                        }
                    }
                }
                if (jokers > 0) handType += (jokers * 2) - ((handType == 0) ? 1 : 0);

                return handType * (int)Math.Pow(16, 5) + handCode;
            }).Select((set, i) => (index: i, set.bid));

            return ordered.Sum(set => (set.index + 1) * set.bid).ToString();
        }
    }
}
