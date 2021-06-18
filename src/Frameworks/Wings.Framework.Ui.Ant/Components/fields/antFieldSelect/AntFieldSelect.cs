using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public partial class AntFieldSelect<TModel> : FieldComponentBase<TModel>
    {
       
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected RenderFragment dynamicFieldSelectComponent => builder =>
        {
            var propModelType = Property.PropertyType;
            var isRequired = Property.GetCustomAttributes(true).Where(attr=>attr.GetType()==typeof(RequiredAttribute)).Count()>0;
            builder.OpenComponent(0, typeof(AntFieldSelectComponent<>).GetGenericTypeDefinition().MakeGenericType(propModelType));
        
            builder.AddAttribute(2, "OnValueChange",
            EventCallback.Factory.Create<object>(this,
            RuntimeHelpers.CreateInferredEventCallback(this, __value =>
            {
                Property.SetValue(Value, __value);
                OnValueChange.InvokeAsync(__value);
            }, new object())));

            builder.AddAttribute(4, "Value", Property.GetValue(Value));
            builder.AddAttribute(5, "AllowClear", isRequired);
            builder.CloseComponent();

        };
    }
}
