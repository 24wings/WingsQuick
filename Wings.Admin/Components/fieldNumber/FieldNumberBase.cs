using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldNumber
{

    public class FieldNumberBase : ComponentBase
    {
        [Parameter]
        public object FieldValue { get; set; }
        public double Value { get; set; }
        protected DisplayAttribute display;

        [Parameter]
        public PropertyInfo Prop { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async Task changeValue()
        {
            switch (Prop.PropertyType.Name)
            {
                case "Int32":
                    await OnValueChange.InvokeAsync((int)Value);
                    break;
                case "Double":
                    await OnValueChange.InvokeAsync(Value);
                    break;

            }

        }

        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
            Value = (int)FieldValue;
            Console.WriteLine(Value);
        }
    }
}