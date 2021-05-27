using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Services;
using Wings.Shared.Attributes;

namespace Wings.Admin.Shared
{
    public class DynamicTableViewBase : ComponentBase
    {
        [Parameter]
        public Type ModelType { get; set; }

        public bool render { get; set; }

        public Type pageType { get; set; }
        protected override void OnInitialized()
        {
            if (!render) render = true;
            var pageAttribute = ModelType.GetCustomAttribute<PageAttribute>(true);
            pageType = PageRegisterFactory.GetPageDefaultComponent(pageAttribute.GetType());


        }
        public RenderFragment dynamicTableComponent => builder =>
   {
       var treeViewComponentType = Assembly.GetExecutingAssembly().DefinedTypes.First(type => type.Name.Contains("TableView") && !type.Name.Contains("Base") && !type.Name.Contains("Dynamic")).MakeGenericType(ModelType);
       builder.OpenComponent(0, treeViewComponentType);
       builder.CloseComponent();
   };
    }
}