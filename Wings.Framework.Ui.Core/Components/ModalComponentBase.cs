using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Framework.Ui.Core
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