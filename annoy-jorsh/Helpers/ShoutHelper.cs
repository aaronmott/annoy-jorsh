using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace annoyjorsh.Helpers
{
    class ShoutHelper
    {
        private readonly static Dictionary<string, string> shouts =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                File.ReadAllText(
                    Path.Combine(Directory.GetCurrentDirectory(), "Data/shouts.json")
                )
            );
        private static Random rand = new Random();
        public static KeyValuePair<string,string> getRandomShout()
        {
            return shouts.ElementAt(rand.Next(0, shouts.Count));
        }
        public static KeyValuePair<string, string> searchShouts(IEnumerable<string> searchWords)
        {
            var entries = shouts.Where(e => searchWords.Any(search => e.Key.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                .Select(e => e)
                .ToList();
            if (entries.Count > 0)
            {
                return entries[rand.Next(entries.Count)];
            } else
            {
                entries = shouts.Where(e => searchWords.Any(search => e.Value.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0))
                .Select(e => e)
                .ToList();
                if (entries.Count > 0)
                {
                    return entries[rand.Next(entries.Count)];
                }
                else
                {
                    return getRandomShout();
                }
            }

        }
    }
}
