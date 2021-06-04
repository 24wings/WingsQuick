using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Attributes;
using Wings.Framework.Ui.Core.Services;
using System.Linq;
using System;

namespace Wings.Framework.Ui.Core.Components
{
    [BuiltinComponent("树形视图")]
    public abstract class TreeViewBase<TModel> : ModelComponentBase<TModel>
    {
        [Inject]
        protected StateContainer stateContainer { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            stateContainer.OnChange += refresh;

        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        protected void refresh()
        {
            componentType = DynamicComponentScanner.ComponentPairs.Where(pair => pair.Active == true && pair.ComponentType.HasImplementedRawGeneric(typeof(TreeViewBase<>))).FirstOrDefault().ComponentType.MakeGenericType(typeof(TModel));
            StateHasChanged();
        }


        protected Type componentType = DynamicComponentScanner.ComponentPairs.Where(pair => pair.Active == true && pair.ComponentType.HasImplementedRawGeneric(typeof(TreeViewBase<>))).FirstOrDefault().ComponentType.MakeGenericType(typeof(TModel));
        public virtual RenderFragment dynamicEditComponent => builder =>
                  {

                      Console.WriteLine("treeView find such as component:" + componentType);
                      builder.OpenComponent(0, componentType);

                      builder.CloseComponent();

                  };


    }

}