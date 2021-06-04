using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Framework.Ui.Core.Components
{
    public abstract class ModelComponentBase<TModel> : DynamicComponentBase
    {
        public override string Category { get; set; } = "视图组件";

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