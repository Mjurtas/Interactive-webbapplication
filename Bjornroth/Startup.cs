using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Repositories;
using Bjornroth.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Bjornroth.Models.DTO;
using System.Text.Json;

namespace Bjornroth
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;

            // Deletes existing file, to get a new updates list of movies.
            //if (File.Exists("../json/movies.json"))
            //{
            //    File.Delete("../movies.json");
                //List<MovieDTO> movies = await cmdbRepository.GetMovies();
                //string jsonString = JsonSerializer.Serialize(movies);
                //File.WriteAllText("movies.json", jsonString);
                //Movies = (List<MovieDTO>)FileOperations.Deserialize("../json/movies.json");
            //}

            //var output = cmdbRepository.GetMovies();

            //File.WriteAllText("movies.json", output);

        }
        //public List<MovieDTO> Movies { get; set; }
        public IConfiguration Configuration { get; }
     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ICmdbRepository, CmdbRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Start/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Start}/{action=Index}/{id?}");
            });
        }
    }
}
