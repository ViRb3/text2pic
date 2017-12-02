using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Text2Pic.Controllers
{
    public class Text2Pic : Controller
    {
        class ParsedWord
        {
            public string Word { get; }
            public string Type { get; }

            public ParsedWord(string word, string type)
            {
                Word = word;
                Type = type;
            }

            public ParsedWord(string word)
            {
                string[] parsedWord = word.Split('/');
                Word = parsedWord[0];
                Type = parsedWord[1];
            }
        }

        private async Task<string[]> GetParsedSentence(string query)
        {
            var request = await Program.HttpClient.GetAsync($"http://localhost:5000/API/ParseSentence/{query}");
            string parsedSentence = await request.Content.ReadAsStringAsync();
            string[] parsed = JsonConvert.DeserializeObject<string[]>(parsedSentence);
            return parsed;
        }

        private List<ParsedWord> ParseWords(string[] parsedSentence)
        {
            List<ParsedWord> parsedWords = new List<ParsedWord>();
            foreach (var word in parsedSentence)
                parsedWords.Add(new ParsedWord(word));
            return parsedWords;
        }

        private async Task<string[]> GetImages(string query)
        {
            var request = await Program.HttpClient.GetAsync($"http://localhost:5000/API/GetImages/{query}");
            string resultUrls = await request.Content.ReadAsStringAsync();
            JArray parsed = JsonConvert.DeserializeObject<JArray>(resultUrls);
            string[] array = parsed.Select(p => (p.First as JProperty).Value.ToString()).ToArray();
            return array;
        }

        public async Task<string> Request(string query)
        {
            var parsedSentence = await GetParsedSentence(query);
            var parsedWords = ParseWords(parsedSentence);

            List<(string, string[])> correctedWords = new List<(string, string[])>();
            foreach (var word in parsedWords)
            {
                string sqlResult = Program.Database.GetUrl(word.Word.ToLower(), word.Type);
                if (!string.IsNullOrEmpty(sqlResult))
                    correctedWords.Add((word.Word, new[] {sqlResult}));
                else
                    correctedWords.Add((word.Word, await GetImages(word.Word)));
            }

            JObject jObject = new JObject();
            foreach (var word in correctedWords)
            {
                JArray array = new JArray();
                foreach (string url in word.Item2)
                {
                    Console.WriteLine(url);
                    array.Add(url.Replace("\\n", ""));
                }

                JProperty wordObject = new JProperty(word.Item1, array);
                jObject.Add(wordObject);
            }
            string result = JsonConvert.SerializeObject(jObject);
            Console.WriteLine(result);
            return result;
        }
    }
}