using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldString
{
    public class FieldStringBase<TModel> : PropertyComponentBase<TModel>
    {
        [Parameter]
        public string FieldValue { get; set; }



        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }


        protected async Task changeValue(object e)
        {
            FieldValue = (string)e;
            Property.SetValue(Value, e);
            await OnValueChange.InvokeAsync(e);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            FieldValue = (string)Property.GetValue(Value);

        }

    }
}
