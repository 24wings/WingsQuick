using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Framework.Shared.Attributes;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Admin.Shared
{
    public class DataSourceManager<TModel> : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private IConfiguration Configuration { get; set; }

        public async Task<object> Insert(TModel data)

        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetValue<string>("url") + dataSource.Insert;
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
            var url = Configuration.GetValue<string>("url") + dataSource.Update;
            return await Post(data, url);

        }

        public async Task<BasicQueryResult<TModel>> Load(BasicQuery query)
        {
            var dataSourceAttribute = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var serverUrl = Configuration.GetValue<string>("url");
            var response = await httpClient.GetAsync(serverUrl + dataSourceAttribute.Load);
            var resString = await response.Content.ReadAsStringAsync();
            var resultType = Assembly.GetAssembly(typeof(BasicQuery)).DefinedTypes.First(type => type.Name.Contains("BasicQueryResult"));
            var resData = JsonSerializer.Deserialize<BasicQueryResult<TModel>>(resString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return resData;
        }

        private async Task<object> Post(TModel data, string url)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync(url, content);
            return await res.Content.ReadAsStringAsync();

        }

        public async Task<object> Delete(TModel data)
        {
            var dataSource = typeof(TModel).GetCustomAttribute<DataSourceAttribute>();
            var url = Configuration.GetValue<string>("url") + dataSource.Delete;
            var res = await httpClient.DeleteAsync(url + "?id=" + data.GetType().GetProperty("Id").GetValue(data));
            return await res.Content.ReadAsStringAsync();

        }

    }

}