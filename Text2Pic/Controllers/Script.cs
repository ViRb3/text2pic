using Microsoft.AspNetCore.Mvc;

namespace Text2Pic.Controllers
{
    [Route("")]
    public class Script : Controller
    {
        [Route("/script.js")]
        public ContentResult Get()
        {
            string responseString = System.IO.File.ReadAllText("www/script.js");
            return new ContentResult
            {
                Content = responseString,
                //ContentType = "text/html"
            };
        }
    }
}