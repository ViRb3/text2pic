using Microsoft.AspNetCore.Mvc;

namespace Text2Pic.Controllers
{
    public class Page : Controller
    {
        public ContentResult Index()
        {
            string responseString = System.IO.File.ReadAllText("www/index.html");
            return new ContentResult
            {
                Content = responseString,
                ContentType = "text/html"
            };
        }
       
    }
}