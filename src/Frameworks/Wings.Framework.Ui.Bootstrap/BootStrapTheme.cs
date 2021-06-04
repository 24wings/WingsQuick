using System.Reflection;
using Wings.Framework.Shared;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Framework.Ui.Bootstrap
{
    public class BootstrapTheme : IThemeConfig
    {
        public string ThemeName { get; set; } = "Bootstrap主题";
        public PropConfig DefaultPropConfig { get; set; } = new PropConfig
        {

        };
        public FieldConfig DefaultFieldConfig { get; set; } = new FieldConfig
        {

        };

        public void UseCurrentTheme()
        {
            DynamicComponentScanner.CurrentTheme = new BootstrapTheme();
            var dynamicComponentPairs = DynamicComponentScanner.ScanDynmaicComponentByAssembly(Assembly.GetExecutingAssembly());
            DynamicComponentScanner.ComponentPairs.ForEach(pair =>
            {
                if (dynamicComponentPairs.FindLastIndex(p => p.ComponentFullName == pair.ComponentFullName) != -1)
                {
                    System.Console.WriteLine("active component pair:" + pair.ComponentFullName);
                    pair.Active = true;
                }
                else
                {
                    System.Console.WriteLine("disabled component pair:" + pair.ComponentFullName);
                    pair.Active = false;
                }
            }
                );

        }
    }

}