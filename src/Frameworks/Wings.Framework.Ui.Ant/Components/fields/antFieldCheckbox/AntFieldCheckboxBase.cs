using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    /// <summary>
    /// checkbox表单字段
    /// </summary>
    public abstract class AntFieldCheckboxBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        [Parameter]
        public System.Reflection.PropertyInfo Prop { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async Task changeValue(bool value)
        {
            await OnValueChange.InvokeAsync(value);
        }
        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
        }
    }
}