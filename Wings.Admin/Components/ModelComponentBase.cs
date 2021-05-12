using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Wings.Admin.Components
{
    public class ModelComponentBase<TModel> : ComponentBase
    {

        [Parameter]
        public TModel Value { get; set; }

        public DisplayAttribute Display { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Display = typeof(TModel).GetType().GetCustomAttribute<DisplayAttribute>();
        }
    }
}