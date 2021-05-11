using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Admin.Shared
{
    public class DynamicTreeViewBase : ComponentBase
    {
        [Parameter]
        public Type ModelType { get; set; }

        public bool render { get; set; }
        [Inject]
        private IConfiguration _configuration { get; set; }
        public Type pageType { get; set; }
        protected async override Task OnInitializedAsync()
        {
            if (!render) render = true;
            var pageAttribute = ModelType.GetCustomAttribute<PageAttribute>(true);
            pageType = PageRegisterFactory.GetPageDefaultComponent(pageAttribute.GetType());
            var dataSourceAttribute = ModelType.GetCustomAttribute<DataSourceAttribute>();
            // var response = await _httpClient.GetAsync("http://localhost" + dataSourceAttribute.Url);
            // var resString = await response.Content.ReadAsStringAsync();
            // var resultType = Assembly.GetAssembly(typeof(BasicQuery)).DefinedTypes.First(type => type.Name.Contains("BasicQueryResult"));
            // Console.WriteLine("result Type:" + resultType);
            // var resData = JsonSerializer.Deserialize(resString, resultType.MakeGenericType(ModelType));
            // Console.WriteLine(resData);
            // Console.WriteLine(_configuration.GetValue<string>("url"));
        }
    }
}