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
using Wings.Examples.UseCase.Client.Services;
using Tewr.Blazor.FileReader;
using Wings.Framework.Ui.Core.Services;
using Blazored.LocalStorage;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Wings.Examples.UseCase.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services
                .AddAuthorizationCore()
                 .AddScoped(sp =>
                   new HttpClient
                   {
                       BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                   }
                  )
            .AddAutoMapper(Assembly.GetAssembly(typeof(SharedAutoMapperProfile)))
             .AddScoped<StateContainer>()
               .AddScoped<ConfigService>()
            .AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.IgnoreNullValues = true;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            })
              .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
            .AddScoped<IAuthService, AuthService>()
            //.AddScoped<CompanyService>();
            .AddScoped<MenuService>()
            .AddScoped<LocalStorageService>()
            //.AddScoped<UserService>()
            .AddScoped<ResourceService>()
             .AddScoped<TagsService>()
            .AddFileReaderService()
            .AddAntDesign()
            .AddAntDesignTheme()
            .UseAntDesignTheme();

            await builder.Build().RunAsync();
        }
    }
}
