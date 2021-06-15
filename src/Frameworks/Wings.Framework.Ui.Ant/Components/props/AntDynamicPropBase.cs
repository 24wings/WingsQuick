using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Ui.Core.Components;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntDynamicPropBase<TModel> : PropertyComponentBase<TModel>
    {

        private bool render { get; set; } = false;
        private Type type { get; set; }



        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }
        protected override void OnInitialized()
        {
            render = true;
            base.OnInitialized();
            type = DynamicComponentScanner.GetPropComponentTypeByProperty<TModel>(Property);
            Console.WriteLine("prop type:"+Property.PropertyType+"  type:"+ type);


        }


        protected RenderFragment dynamicComponent => builder =>
        {

            

            builder.OpenComponent(0, type);
            builder.AddAttribute(1, "Property", Property);



            builder.AddAttribute(3, "Value", Value);
            builder.CloseComponent();

        };
    }
}