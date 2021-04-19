using LoafAndStranger.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoafAndStranger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the Inversion of Control(IoC) Container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // tells asp.net that it should know about a cetain type
            /*services.AddTransient<IConfiguration>(); // every time someone asks for an instance, asp.net builds a new one
            services.AddScoped<IConfiguration>(); //the FIRST time someone asks for an instance in a single request, asp.net builds a new one
            services.AddSingleton<IConfiguration>(); */// the first time while the APPLICATION IS RUNNING, asp.net creates a new instance

            services.AddSingleton(Configuration);
            services.AddTransient<StrangersRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
