using System.Reflection;
using Wings.Framework.Ui.Core.Configs;
using Wings.Framework.Ui.Core.Services;
using Wings.Framework.Ui.Bootstrap.Components;
using System;
using Wings.Framework.Ui.Bootstrap;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapTheme(this IServiceCollection services)
        {
            var dynamicComponentPairs = DynamicComponentScanner.ScanDynmaicComponentByAssembly(Assembly.GetExecutingAssembly());
            DynamicComponentScanner.AddComponentPairs(dynamicComponentPairs);

            var bootstrapTheme = new BootstrapTheme();
            DynamicComponentScanner.ThemeList.Add(bootstrapTheme);
            return services;
        }

        public static IServiceCollection UseBootstrapTheme(this IServiceCollection services)
        {
            DynamicComponentScanner.CurrentTheme = new BootstrapTheme();
            DynamicComponentScanner.CurrentTheme.UseCurrentTheme();
            return services;
        }

    }
}