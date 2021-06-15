using System;
using System.Reflection;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Ui.Core.Components;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntDynamicFieldBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        protected bool render { get; set; } = false;


        protected Type type { get; set; }

        public  void Refresh()
        {
            StateHasChanged();
        }

   
        protected override void OnInitialized()
        {
            render = true;
            base.OnInitialized();
            type = DynamicComponentScanner.GetFieldComponentTypeByProperty<TModel>(Property);






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
