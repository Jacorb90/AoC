using AoC.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Runner
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DayAttribute : Attribute
    {
        public readonly int Year;
        public readonly int Day;

        public DayAttribute(int year, int day)
        {
            Year = year;
            Day = day;
        }
    }

    public static class DayManager
    {
        private static readonly Dictionary<(int year, int day), IDay?> DayData = new();

        public static void Setup()
        {
            InputManager.InitHttpClient();

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                var days = a.GetTypes().Select(t => (type: t, attr: t.GetCustomAttribute<DayAttribute>())).Where(p => p.attr is not null && !DayData.ContainsKey((p.attr.Year, p.attr.Day)));
                foreach (var (type, attr) in days)
                {
                    DayData.Add((attr!.Year, attr.Day), Activator.CreateInstance(type) as IDay);
                }
            }
        }

        public static void Run(int year, int day, bool part2, string? overrideInput = null)
        {
            var input = overrideInput ?? InputManager.SaveInput(year, day);
            var output = part2 ? DayData[(year, day)]?.Part2(input) : DayData[(year, day)]?.Part1(input);
            Console.WriteLine("Day " + day + " [" + year + "]: " + (output ?? "NULL"));
        }
    }
}
