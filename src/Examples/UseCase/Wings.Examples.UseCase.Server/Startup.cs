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
using AutoMapper;
using Pomelo.EntityFrameworkCore.MySql;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Wings.Examples.UseCase.Shared.Dvo;
using Microsoft.AspNetCore.Identity;
using Wings.Examples.UseCase.Server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Wings.Examples.UseCase.Server.Services.Repositorys;
using Wings.Examples.UseCase.Server.Services.UnitOfWork;
using Microsoft.AspNetCore.Authentication;

namespace Wings.Examples.UseCase.Server
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
            var connectionString = "server=47.117.86.106;port=3307;user=root;password=704104..;database=ef";
            var serverVersion = new MySqlServerVersion(new Version(5, 0, 1));

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddRazorPages();
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
            services.AddIdentity<RbacUser, RbacRole>()
          .AddEntityFrameworkStores<AppDbContext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddOData();
            services.AddMvc(mvc => { mvc.EnableEndpointRouting = false; });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                    };
                });

            services.AddAuthorization(options => options.AddPolicy("13419597065", policy => policy.RequireClaim(ClaimTypes.Name)));
            services.AddScoped<UnitOfWork>();

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
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();


            app.UseCors("cors");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                // and this line to enable OData query option, for example $filter
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                var builder = new ODataConventionModelBuilder(app.ApplicationServices);

                routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

                // uncomment the following line to Work-around for #1175 in beta1
                // routeBuilder.EnableDependencyInjection();
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    
    }
}
