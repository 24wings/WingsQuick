using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Framework.Ui.Core.Components
{
    public class ModelComponentBase<TModel> : DynamicComponentBase
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