using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Framework.Ui.Core.Components;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Framework.Ui.Core.Components
{
    public abstract class DynamicViewBase<TModel> : ModelComponentBase<TModel>
    {
        [Parameter]
        public TModel SelectedData { get; set; }
        [Inject]
        protected StateContainer stateContainer { get; set; }
        protected override void OnInitialized()
        {
            GetViewComponentType();
            base.OnInitialized();
            stateContainer.OnChange += refresh;

        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected void refresh()
        {
            GetViewComponentType();
            StateHasChanged();
        }


        public void GetViewComponentType()
        {
            componentType = DynamicComponentScanner.GetViewComponentType(typeof(TModel)).GetGenericTypeDefinition().MakeGenericType(typeof(TModel));
        }


        protected Type componentType { get; set; }
        public RenderFragment dynamicComponent => builder =>
        {
            GetViewComponentType();


            builder.OpenComponent(0, componentType);


       //     builder.AddAttribute(2, nameof(TreeView<>.) "OnValueChange",
       //EventCallback.Factory.Create<object>(this,
       //RuntimeHelpers.CreateInferredEventCallback(this, __value =>
       //{
       //    SelectedData =
       //__value; OnValueChange.InvokeAsync(FieldValue);
       //}, FieldValue)));

            builder.CloseComponent();


        };

    }
}
