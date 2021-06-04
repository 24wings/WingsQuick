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
        public PropConfig DefaultPropConfig { get; set; } = new PropConfig
        {
            PropStringComponent = new ComponentPair(typeof(AntPropString<object>)),
            PropBoolComponent = new ComponentPair(typeof(AntPropString<object>)),
            PropNumberComponent = new ComponentPair(typeof(AntPropString<object>)),
            PropTreeViewComponent = new ComponentPair(typeof(AntPropTreeView<object>))
        };

        public FieldConfig DefaultFieldConfig { get; set; } = new FieldConfig
        {
            FieldStringComponent = new ComponentPair(typeof(AntFieldString<object>)),
            FieldBoolComponent = new ComponentPair(typeof(AntFieldCheckbox<object>)),
            FieldDateComponent = new ComponentPair(typeof(AntFieldDate<object>)),
            FieldNumberComponent = new ComponentPair(typeof(AntFieldNumber<object>)),
            FieldTreeViewComponent = new ComponentPair(typeof(AntFieldTreeSelect<object>)),

        };

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