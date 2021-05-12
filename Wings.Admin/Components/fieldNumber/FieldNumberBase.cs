using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldNumber
{

    public class FieldNumberBase<TModel> : PropertyComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        public double MyValue { get; set; }
    

       
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async Task changeValue()
        {
            switch (Property.PropertyType.Name)
            {
                case "Int32":
                    await OnValueChange.InvokeAsync((int)MyValue);
                    break;
                case "Double":
                    await OnValueChange.InvokeAsync(MyValue);
                    break;

            }

        }

        protected override void OnInitialized()
        {

            base.OnInitialized();
        }
    }
}