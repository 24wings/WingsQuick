using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using System.Threading;
using Wings.Admin.Services;

namespace Wings.Admin.Pages
{
    public class DynamicPageBase : ComponentBase
    {
        public bool render { get; set; }
        [Parameter]
        public string ClassName { get; set; }
        public Dictionary<string, object> prps { get; set; } = new Dictionary<string, object>();
        public TypeInfo type { get; set; }
        HttpClient _httpClient { get; }
        public Type pageType { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected PageAttribute pageAttribute;


        public DynamicPageBase()
        {

        }

        protected override void OnInitialized()
        {
            if (!render) render = true;
            RefershRenderComponent();

            NavigationManager.LocationChanged += HandleLocationChanged;

        }
        private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
        {
            render = false;
            Console.WriteLine("navigation location change");
            StateHasChanged();
            RefershRenderComponent();
            render = true;
            StateHasChanged();
        }

        private void RefershRenderComponent()
        {
            type = Assembly.GetAssembly(typeof(Wings.Shared.Dvo.DateRange)).DefinedTypes.FirstOrDefault(typeInfo => typeInfo.Name ==
           ClassName);
            if (type == null)
            {
                throw new Exception("未找到当前类");
            }

            pageAttribute = type.GetCustomAttribute<PageAttribute>(true);
            pageType = PageRegisterFactory.GetPageDefaultComponent(pageAttribute.GetType());
        }

        public RenderFragment dynamicComponent => builder =>
         {



             builder.OpenComponent(0, pageType);
             builder.AddAttribute(1, "ModelType", type);


             builder.CloseComponent();

         };
    }
}