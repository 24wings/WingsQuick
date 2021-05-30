using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.fieldIcon
{
    public abstract class FieldIconBase<TModel> : PropertyComponentBase<TModel>
    {
        protected bool ShowIconPickerModal { get; set; }
        [Parameter]
        public object FieldValue { get; set; }
        public string MyValue { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }


        protected override void OnInitialized()
        {

            base.OnInitialized();
        }
        protected async Task ChangeIcon(string value)
        {
            Property.SetValue(Value, value);
            await OnValueChange.InvokeAsync(value);

        }
    }


}