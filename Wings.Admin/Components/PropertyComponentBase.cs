using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components
{
    public class PropertyComponentBase<TModel> : ComponentBase
    {
        [Parameter]
        public TModel Value { get; set; } = default;
        [Parameter]
        public PropertyInfo Property { get; set; }
    }
}