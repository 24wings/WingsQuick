using Wings.Framework.Shared;
using Wings.Framework.Ui.Ant.Components;
using Wings.Framework.Shared;
using Wings.Framework.Ui.Core.Services;
using System.Reflection;

namespace Wings.Framework.Ui.Ant
{
    public class AntDeisgnTheme : IThemeConfig
    {
        public string ThemeName { get; set; } = "AntDesign主题";
      
        public void UseCurrentTheme()
        {
            DynamicComponentScanner.CurrentTheme = new AntDeisgnTheme();
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