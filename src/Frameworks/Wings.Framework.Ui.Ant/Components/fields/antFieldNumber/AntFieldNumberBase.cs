using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{

    public abstract class AntFieldNumberBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        public double MyValue { get; set; }





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