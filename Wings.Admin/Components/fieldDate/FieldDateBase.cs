using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldDate
{
    /// <summary>
    /// 日期表单字段
    /// </summary>
    public class FieldDateBase : ComponentBase
    {
        [Parameter]

        public DateTime FieldValue { get; set; }

        protected DisplayAttribute display;

        [Parameter]
        public PropertyInfo Prop { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async Task changeValue()
        {


        }

        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
        }
    }
}