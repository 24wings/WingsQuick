using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldCheckbox
{
    /// <summary>
    /// checkbox表单字段
    /// </summary>
    public class FieldCheckboxBase : ComponentBase
    {
        [Parameter]
        public object FieldValue { get; set; }
        protected DisplayAttribute display;
        public bool Value { get; set; }
        [Parameter]
        public System.Reflection.PropertyInfo Prop { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async Task changeValue(bool value)
        {
            Value = value;
            await OnValueChange.InvokeAsync(value);
        }
        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
        }
    }
}