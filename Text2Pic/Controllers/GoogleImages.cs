using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace Text2Pic.Controllers
{
    public class GoogleImages : Controller
    {
        public async Task<string> Request([FromServices] INodeServices nodeServices, string query)
        {
            var result = await nodeServices.InvokeAsync<string>("magic.js", query);
            return result;
        }
    }
}