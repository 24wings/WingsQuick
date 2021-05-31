using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    /// <summary>
    /// 日期表单字段
    /// </summary>
    public class AntFieldDateBase<TModel> : FieldComponentBase<TModel>
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