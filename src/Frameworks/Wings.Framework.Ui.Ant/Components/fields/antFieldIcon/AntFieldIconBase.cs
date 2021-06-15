using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntFieldIconBase<TModel> : FieldComponentBase<TModel>
    {
        protected bool ShowIconPickerModal { get; set; }
        [Parameter]
        public object FieldValue { get; set; }
        public string MyValue { get; set; }
 


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