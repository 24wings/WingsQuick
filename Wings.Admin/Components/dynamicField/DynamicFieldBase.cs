using System;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components;
using Wings.Shared.Attributes;
using Wings.Admin.Services;

namespace Wings.Admin.Components.dynamicField
{
    public class DynamicFieldBase<TModel> : PropertyComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        protected bool render { get; set; } = false;


        protected Type type { get; set; }



        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }
        protected override void OnInitialized()
        {
            render = true;
            base.OnInitialized();
            if (Property.PropertyType.IsGenericType)
            {
                var fieldAttribute = Property.GetCustomAttribute<FormFieldAttribute>();
                type = ComponentRegisterFactory.GetFieldDefaultComponent(fieldAttribute.GetType());
                type = type.GetGenericTypeDefinition().MakeGenericType(typeof(TModel));

            }
            else
            {
                type = ComponentRegisterFactory.GetFieldDefaultComponent(Property.PropertyType);
            }





        }


        protected RenderFragment dynamicComponent => builder =>
        {
            builder.OpenComponent(0, type);
            builder.AddAttribute(1, "Property", Property);
            builder.AddAttribute(2, "OnValueChange",
        EventCallback.Factory.Create<object>(this,
        RuntimeHelpers.CreateInferredEventCallback(this, __value =>
        {
            FieldValue =
        __value; OnValueChange.InvokeAsync(FieldValue);
        }, FieldValue)));

            builder.AddAttribute(4, "Value", Value);
            builder.CloseComponent();

        };
    }
}
