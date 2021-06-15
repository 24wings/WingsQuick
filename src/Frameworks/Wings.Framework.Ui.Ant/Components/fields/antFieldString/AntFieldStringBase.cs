using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntFieldStringBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public string FieldValue { get; set; }






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