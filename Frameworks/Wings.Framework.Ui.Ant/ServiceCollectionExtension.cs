using System.Reflection;
using Wings.Framework.Ui.Core.Configs;
using Wings.Framework.Ui.Core.Services;
using Wings.Framework.Ui.Ant.Components;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntDesignTheme(this IServiceCollection services)
        {
            var dynamicComponentPairs = DynamicComponentScanner.ScanDynmaicComponentByAssembly(Assembly.GetExecutingAssembly());
            DynamicComponentScanner.AddComponentPairs(dynamicComponentPairs);
            foreach (var pair in dynamicComponentPairs)
            {
                Console.WriteLine(pair.ComponentType + ":" + pair.ComponentDisplayName + ":" + pair.DataType);
            }
            Console.WriteLine("scanning asembly:" + Assembly.GetExecutingAssembly());
            //替换 属性控件
            AppControlConfig.propDefaultControl.StringControl = typeof(AntPropString<object>).FullName;
            AppControlConfig.propDefaultControl.NumberControl = typeof(AntPropString<object>).FullName;

            return services;
        }
    }
}