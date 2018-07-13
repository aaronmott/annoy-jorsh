using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using annoyjorsh.Models;
using Newtonsoft.Json;

namespace annoyjorsh.Helpers
{
    public static class InsultGenerator
    {
        private readonly static IQueryable<InsultPart> parts =
            JsonConvert.DeserializeObject<List<InsultPart>>(
                File.ReadAllText(
                    Path.Combine(Directory.GetCurrentDirectory(), "Data/insult_parts.json")
                )
            ).AsQueryable();
        private static Random rand = new Random();

        public static List<InsultPart> getAllInsultParts()
        {
            return parts.ToList();
        }

        public static string getQuickInsult(int totalSyllables = 4, bool plural = false)
        {
            var currentSyllableCount = 0;
            var returnString = "";
            var rand = new Random();
            var tempParts = parts;
            while(currentSyllableCount < totalSyllables){
                var part = tempParts.Random(p => currentSyllableCount +  p.syllables <= totalSyllables );
                currentSyllableCount += part.syllables;
                returnString += (currentSyllableCount == totalSyllables && plural) ? part.many : part.phrase;
                returnString += currentSyllableCount < totalSyllables ? " " : ""; 
                tempParts = tempParts.Where(p => p.phrase != part.phrase);
            }
            return returnString;
        }
        
        public static T Random<T>(this IQueryable<T> q, Expression<Func<T,bool>> e)
        {
            q  = q.Where(e);
            return q.Skip(rand.Next(q.Count())).FirstOrDefault();
        }
    }
}