using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Text2Pic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddNodeServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "",
                    defaults: new {controller = "Page", action = "Index"});
                routes.MapRoute(
                    name: "googleimages",
                    template: "API/GetImages/{query}",
                    defaults: new {controller = "GoogleImages", action = "Request"});
                routes.MapRoute(
                    name: "stanfordapi",
                    template: "API/ParseSentence/{query}",
                    defaults: new {controller = "StanfordAPI", action = "Request"});
                routes.MapRoute(
                    name: "text2pic",
                    template: "API/Text2Pic/{query}",
                    defaults: new {controller = "Text2Pic", action = "Request"});
            });
        }
    }
}