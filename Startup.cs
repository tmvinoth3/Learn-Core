using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using web_hello.Models;

namespace web_hello
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;            
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddMvcCore();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // app.Use(async (context, next) => 
            // {
            //     logger.LogInformation("MiddleWare 1 Incoming Request");
            //     await context.Response.WriteAsync("MW1 method called");
            //     await next();
            //     logger.LogInformation("MiddleWare 1 Outgoing Response");
            // });

            // app.Use(async (context, next) => 
            // {
            //     logger.LogInformation("MiddleWare 2 Incoming Request");
            //     await context.Response.WriteAsync("MW2 method called");
            //     await next();
            //     logger.LogInformation("MiddleWare 2 Outgoing Response");
            // });

            // PhysicalFileProvider fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory()));
            // DefaultFilesOptions defoptions = new DefaultFilesOptions();
            // defoptions.DefaultFileNames.Clear();
            // defoptions.FileProvider = fileProvider;
            // defoptions.DefaultFileNames.Add("index.html");
            
            //app.UseDefaultFiles(defoptions);
            app.UseStaticFiles();
            //app.UseFileServer();
            //app.UseMvcWithDefaultRoute();
            
            // app.Run(async (context) =>
            // {
            //     //throw new Exception("Error thrown from Run method");
            //     await context.Response.WriteAsync(env.EnvironmentName);
            // });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}"
                );
                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World");
                // });
                                
            });

        }
    }
}
