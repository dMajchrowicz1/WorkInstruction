using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConveyorDoc.Business.UseCase
{
    public static class RegexTool 
    {
        public static List<string> GetMatches(string[] lines, string pattern)
        {
            var result  = new List<string>();

            foreach (var line in lines)
            {
                var taggedWords = Regex.Matches(line, pattern).Cast<Match>().ToList();

                foreach (var word in taggedWords)
                {
                    result.Add(word.Value);
                }
            }

            return result;
        }


        public static string GetMatch(string text, string pattern)
        {
            return Regex.Matches(text, pattern).Cast<Match>().ToString();
        }

    }
}
