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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Wings.Examples.UseCase.Server.Services;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.OpenApi.Models;

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
            services.AddOData();

            var connectionString = "server=localhost;port=3306;user=root;password=704104..;database=ef; ConvertZeroDateTime=True";
            var serverVersion = new MySqlServerVersion(new Version(5, 0, 1));

            services.AddControllersWithViews(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
            })
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
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,opt=>
                {
                    opt.AccessDeniedPath = "/Login";
                    opt.Cookie.HttpOnly =false;
                })
                ;

            services.AddAuthorization(options =>
            {

                options.AddPolicy("13419597065", policy => policy.RequireClaim(ClaimTypes.Name));
                

                });
            services.AddScoped<TokenService>();
            services.AddScoped<UnitOfWork>();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = false;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                //options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //options.SlidingExpiration = true;
            });
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var sharedXmlPath = Path.Combine(AppContext.BaseDirectory, "Wings.Examples.UseCase.Shared.xml");
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(sharedXmlPath);
                
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                c.EnableFilter();
                c.EnableValidator();
              
                
            });
            app.UseWebAssemblyDebugging();
            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();


            app.UseCors("cors");
            app.UseAuthentication().UseCookiePolicy(new CookiePolicyOptions { HttpOnly = HttpOnlyPolicy.None, MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None });
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                // and this line to enable OData query option, for example $filter
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                var builder = new ODataConventionModelBuilder(app.ApplicationServices);

                //routeBuilder.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

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
