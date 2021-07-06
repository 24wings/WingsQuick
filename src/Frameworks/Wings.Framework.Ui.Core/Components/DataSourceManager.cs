using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Data;

namespace Wings.Framework.Ui.Core.Components
{
    public class DataSourceManager<TModel> : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        [Parameter]
        public ODataAdapter<TModel> oDataAdapter { get; set; }
        [Parameter]
        public int DefaultPageSize { get; set; } = 10;

        [Inject]
        private IConfiguration Configuration { get; set; }
        protected override void OnInitialized()
        {
            var dataSourceAttribute = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            if (oDataAdapter == null)
            {
                if (dataSourceAttribute != null)
                {
                    var a = Configuration.GetSection("ConnectionStrings");
                    Console.WriteLine("key" + a.Key + ":" + a.Value);
                    var dataAdapterOptions = new DataAdapterOptions { LoadUrl = Configuration.GetConnectionString("url") + dataSourceAttribute.LoadUrl ,PageSize=dataSourceAttribute.PageSize|10};
                    Console.WriteLine(dataAdapterOptions.LoadUrl);
                    oDataAdapter = new ODataAdapter<TModel>(dataAdapterOptions,httpClient);

                }
            }
        }

        public async Task<object> Insert(TModel data)

        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetConnectionString("url") + dataSource.InsertUrl;
            return await Post(data, url);

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<object> Update(TModel data)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetConnectionString("url") + dataSource.Update;
            return await Post(data, url);

        }

        // public async Task<BasicQueryResult<TModel>> Load(BasicQuery query)
        // {
        //     var dataSourceAttribute = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
        //     var serverUrl = Configuration.GetValue<string>("url");
        //     Console.WriteLine("dataSourceAttribute:" + dataSourceAttribute);
        //     var response = await httpClient.GetAsync(serverUrl + dataSourceAttribute.Load);
        //     var resString = await response.Content.ReadAsStringAsync();
        //     var resultType = Assembly.GetAssembly(typeof(BasicQuery)).DefinedTypes.First(type => type.Name.Contains("BasicQueryResult"));
        //     var resData = JsonSerializer.Deserialize<BasicQueryResult<TModel>>(resString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //     return resData;
        // }
        public async Task<Paged<TModel>> Load(List<WhereConditionPair> whereConditionPairs)
        {
            var rtn = await oDataAdapter.LoadAsync(whereConditionPairs);
            return rtn;
        }
        public async Task<Paged<TModel>> Load(int pageIndex=0)
        {
            
            var emptyWhereCondition = new List<WhereConditionPair>();
            var rtn = await oDataAdapter.LoadAsync(emptyWhereCondition);
            return rtn;
        }

        private async Task<object> Post(TModel data, string url)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(url, content);
            return await res.Content.ReadAsStringAsync();

        }

        public async Task<TModel> Detail(int id)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetConnectionString("url") + dataSource.Detail;
            var res = await httpClient.GetStringAsync(url + "?id=" +id);
            Console.WriteLine(res);
            return JsonSerializer.Deserialize<TModel>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<object> Delete(TModel data)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetConnectionString("url") + dataSource.Delete;
            var res = await httpClient.DeleteAsync(url + "?id=" + data.GetType().GetProperty("Id").GetValue(data));
            return await res.Content.ReadAsStringAsync();

        }

    }

}