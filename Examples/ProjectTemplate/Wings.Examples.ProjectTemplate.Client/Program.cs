using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wings.Shared;
using System.Reflection;

using Wings.Admin.Services;
using Blazored.LocalStorage;
using System.Text.Json;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Examples.UseCase.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(SharedAutoMapperProfile)));
            builder.Services
            .AddSingleton<HttpClient>()

            .AddAntDesign()

            .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
             .AddScoped<Wings.Framework.Ui.Core.Services.MenuService>()
             .AddScoped<Wings.Framework.Ui.Core.Services.ConfigService>()
                .AddScoped<Wings.Framework.Ui.Core.Services.LocalStorageService>()
            .AddScoped<Wings.Admin.Services.ConfigService>()
            .AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.IgnoreNullValues = true;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            }

)
     .AddAntDesignTheme();
            var componentPairs = DynamicComponentScanner.ComponentPairs;
            await builder.Build().RunAsync();
        }
    }
}
