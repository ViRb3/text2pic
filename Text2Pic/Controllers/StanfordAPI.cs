using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Text2Pic
{
    public class StanfordAPI : Controller
    {
        private string requestURL = "http://nlp.stanford.edu:8080/parser/index.jsp";

        private async Task<string> ReadResponse(string sentence)
        {
            var values = new Dictionary<string, string>
            {
                {"query", sentence}
            };

            var content = new FormUrlEncodedContent(values);
            var response = await Program.HttpClient.PostAsync(requestURL, content);

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<List<string>> Request(string query)
        {
            List<string> result = new List<string>();
            var doc = new HtmlDocument();
            doc.LoadHtml(await ReadResponse(query));
            var node = doc.DocumentNode.Descendants().First(c => c.Name == "div" && c.HasClass("parserOutputMonospace"));
            foreach (var item in node.ChildNodes.Where(n => n.Name == "div"))
            {
                result.Add(item.FirstChild.InnerHtml.Trim());
            }

            return result;
        }
    }
}