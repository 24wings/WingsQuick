using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wings.Api.Models;
using AutoMapper;
using Pomelo.EntityFrameworkCore.MySql;
using System.Reflection;
using Newtonsoft.Json;


namespace Wings.Api
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
            var connectionString = "server=localhost;user=root;password=704104..;database=ef";
            var serverVersion = new MySqlServerVersion(new Version(5, 0, 1));

            services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddDbContext<AppDbContext>(
                opt => opt.UseLazyLoadingProxies().UseMySql(connectionString, serverVersion)
                .EnableSensitiveDataLogging() // These two calls are optional but help
                    .EnableDetailedErrors() // with debugging (remove for production).;

                    );
            services.AddCors(options =>
options.AddPolicy("cors",
p =>
p.WithOrigins("http://localhost:5000")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("cors");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
