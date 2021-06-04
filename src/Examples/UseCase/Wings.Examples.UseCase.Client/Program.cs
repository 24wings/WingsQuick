using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Wings.Examples.UseCase.Shared;
using Wings.Framework.Ui.Core;

namespace Wings.Examples.UseCase.Client
{ 
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services
            .AddAutoMapper(Assembly.GetAssembly(typeof(SharedAutoMapperProfile)))
             .AddScoped<StateContainer>()
            .AddAntDesign()
            .AddAntDesignTheme()
            .UseAntDesignTheme()
            .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
