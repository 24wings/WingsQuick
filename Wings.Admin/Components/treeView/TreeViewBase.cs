using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;
using Wings.Shared.Dvo;
using Wings.Shared.Attributes;


namespace Wings.Admin.Components
{
    public enum TreeViewState
    {
        List,
        Create,
        Update,
        Delete
    }
    public class TreeViewBase<TModel> : ComponentBase
    {



        public bool render { get; set; }


        public List<TModel> DataListTItem { get; set; } = new List<TModel>();
        [Inject]
        private IConfiguration _configuration { get; set; }
        protected HttpClient _httpClient { get; set; } = new HttpClient();
        public List<BasicTree> DataListPure { get; set; } = new List<BasicTree>();

        protected TModel selectedData { get; set; }
        [Inject]
        protected IMapper mapper { get; set; }

        protected CRUDModelAttribute CRUDModel { get; set; }
        protected Type EditModelType { get; set; }
        protected object EditValue { get; set; }


        protected async override Task OnInitializedAsync()
        {
            if (!render)
            {
                render = true;
                var pageAttribute = typeof(TModel).GetCustomAttribute<PageAttribute>(true);
                var dataSourceAttribute = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
                var serverUrl = _configuration.GetValue<string>("url");
                var response = await _httpClient.GetAsync(serverUrl + dataSourceAttribute.Load);
                var resString = await response.Content.ReadAsStringAsync();
                var resultType = Assembly.GetAssembly(typeof(BasicQuery)).DefinedTypes.First(type => type.Name.Contains("BasicQueryResult"));
                dynamic resData = JsonSerializer.Deserialize(resString,
                resultType.MakeGenericType(typeof(TModel)), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                foreach (var item in resData.Data)
                {
                    var st = JsonSerializer.Serialize(item);
                    DataListTItem.Add(JsonSerializer.Deserialize<TModel>(st));
                }

                CRUDModel = typeof(TModel).GetCustomAttribute<CRUDModelAttribute>();
                EditModelType = CRUDModel.Create;

                StateHasChanged();
            }

        }

        public List<TModel> GetChildren(TModel data)
        {
            var children = data.GetType().GetProperty("Children").GetValue(data);
            return data.GetType().GetProperty("Children").GetValue(data) as List<TModel>;
        }

        public void OpenAddMenuForm()
        {

            var dest = mapper.Map(selectedData, typeof(TModel), CRUDModel.Create);
            Console.WriteLine(JsonSerializer.Serialize(dest));
            EditValue = dest;
        }

    }

}