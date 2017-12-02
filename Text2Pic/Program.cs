using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Text2Pic
{
    public class Program
    {
        internal static readonly HttpClient HttpClient = new HttpClient();
        internal static readonly Database Database = new Database("database.db");
        
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}