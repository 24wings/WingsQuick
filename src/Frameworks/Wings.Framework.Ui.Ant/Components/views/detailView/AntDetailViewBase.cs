using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public class DetailViewBase<TModel> : ComponentBase
    {

        public List<PropertyInfo> PropertyList { get; set; } = new List<PropertyInfo>();
        protected bool render { get; set; } = false;
        [Parameter]
        public TModel Value { get; set; }

        protected override void OnInitialized()
        {
            // first render
            if (!render)
            {
                PropertyList.AddRange(typeof(TModel).GetProperties());
            }
        }


    }
}