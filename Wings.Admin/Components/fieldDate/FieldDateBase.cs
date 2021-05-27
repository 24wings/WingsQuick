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
    public class FieldDateBase<TModel> : PropertyComponentBase<TModel>
    {
        [Parameter]

        public DateTime FieldValue { get; set; }



        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }



        protected override void OnInitialized()
        {

            display = Property.GetCustomAttribute<DisplayAttribute>();
        }
    }
}