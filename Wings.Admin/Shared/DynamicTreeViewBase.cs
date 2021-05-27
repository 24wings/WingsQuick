using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Admin.Services;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Admin.Shared
{
    public class DynamicTreeViewBase : ComponentBase
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

        public RenderFragment dynamicTreeComponent => builder =>
        {
            var treeViewComponentType = Assembly.GetExecutingAssembly().DefinedTypes.First(type => type.Name.Contains("TreeView") && !type.Name.Contains("Base") && !type.Name.Contains("Dynamic")).MakeGenericType(ModelType);
            builder.OpenComponent(0, treeViewComponentType);
            builder.CloseComponent();
        };
    }
}