using System.Reflection;
using Wings.Framework.Ui.Core.Configs;
using Wings.Framework.Ui.Core.Services;
using Wings.Framework.Ui.Ant.Components;
using System;
using Wings.Framework.Ui.Ant;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntDesignTheme(this IServiceCollection services)
        {
            var dynamicComponentPairs = DynamicComponentScanner.ScanDynmaicComponentByAssembly(Assembly.GetExecutingAssembly());

            DynamicComponentScanner.AddComponentPairs(dynamicComponentPairs);

            var antDesignTheme = new AntDeisgnTheme();
            DynamicComponentScanner.ThemeList.Add(antDesignTheme);
            return services;
        }

        public static IServiceCollection UseAntDesignTheme(this IServiceCollection services)
        {

            DynamicComponentScanner.CurrentTheme = new AntDeisgnTheme();
            DynamicComponentScanner.CurrentTheme.UseCurrentTheme();

            return services;
        }

    }
}