using AoC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Inputs
{
    public static class InputManager
    {
        private static HttpClient? Client;

        public static void InitHttpClient()
        {
            Uri address = new("https://adventofcode.com");
            CookieContainer cookieContainer = new();
            cookieContainer.Add(address, new Cookie("session", Token.AoCToken));

            Client = new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer })
            {
                BaseAddress = address
            };
        }

        public static string SaveInput(int year, int day)
        {
            var input = String.Join('\n', Client?.GetStringAsync($"/{year}/day/{day}/input")
                .GetAwaiter().GetResult().Split('\n')[..^1] ?? Array.Empty<string>());
            if (!Directory.Exists($"Input/{year}")) Directory.CreateDirectory($"Input/{year}");
            File.WriteAllText($"Input/{year}/{day}.txt", input);
            return input ?? "???";
        }
    }
}