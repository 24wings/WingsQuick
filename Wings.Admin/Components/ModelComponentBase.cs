using System;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components
{
    public class ModelComponentBase<TModel> : ComponentBase
    {

        [Parameter]
        public TModel Value { get; set; }
    }
}