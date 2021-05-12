using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Wings.Shared.Dvo;

namespace Wings.Admin.Components.dynamicProp
{
    public class DynamicPropBase<TModel> : PropertyComponentBase<TModel>
    {

        private bool render { get; set; } = false;
        private Type type { get; set; }



        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }
        protected override void OnInitialized()
        {
            render = true;
            base.OnInitialized();
            var com = Property.PropertyType.GetCustomAttribute<ComAttribute>();
            if (com != null)
            {
                type = com.type;
            }
            else
            {
                var originType = ComponentRegisterFactory.GetPropDefaultComponent(Property.PropertyType);
                Console.WriteLine("originType:" + originType);
                Console.WriteLine("originType change:" + originType.GetGenericTypeDefinition().MakeGenericType(typeof(TModel)));
                type = originType.GetGenericTypeDefinition().MakeGenericType(typeof(TModel));
            }



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