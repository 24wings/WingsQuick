using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{

    public class AntOption
    {
        public string Label { get; set; }
        public object Value { get; set; }
    }

    public abstract class AntFieldEnumBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
    
        [Parameter]
        public System.Reflection.PropertyInfo Prop { get; set; }

        public List<AntOption> Options { get; set; } = new List<AntOption> { };
        public object SelectedValue { get; set; }

        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
            if (Prop.PropertyType.IsEnum)
            {
                var props = Prop.PropertyType.GetEnumNames();
                foreach (var prop in props)
                {
                    var propDisplay = Prop.PropertyType.GetField(prop).GetCustomAttribute<DisplayAttribute>();
                    Options.Add(new AntOption { Label = propDisplay.Name, Value = (object)prop });
                }
            }
            else
            {
                Console.WriteLine("not enum");
            }
        }
        protected void changeValue(AntOption value)

        {
            Console.WriteLine("选择了:" + value.Value);
            OnValueChange.InvokeAsync(Enum.Parse(Prop.PropertyType, value.Value.ToString()));
        }
    }
}