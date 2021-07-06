using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Wings.Framework.Shared.Dtos;
using System;
using Wings.Framework.Shared.Attributes;

namespace Wings.Framework.Ui.Core.Data
{
    public class DataAdapterOptions
    {
        public string LoadMethod { get; set; }
        public string LoadUrl { get; set; }
        public int PageSize { get; set; }
    }
    public interface IDataAdapter<T>
    {
        /// <summary>
        /// load data
        /// </summary>
        /// <returns></returns>
        Task<Paged<T>> LoadAsync(List<WhereConditionPair> query);
        /// <summary>
        /// load data by query
        /// </summary>
        /// <returns></returns>
        Task<Paged<T>> LoadAsync(List<WhereConditionPair> query, int top, int skip);
    }
    public class ODataAdapter<T> : IDataAdapter<T>
    {
        public DataAdapterOptions options { get; set; }
        public HttpClient httpClient { get; set; }
        public ODataAdapter(DataAdapterOptions _options,HttpClient _httpClient)
        {
            options = _options;
            httpClient = _httpClient;


        }
        public Task<Paged<T>> LoadAsync(List<WhereConditionPair> queryObject)
        {
            return LoadAsync(queryObject, options.PageSize, 0);
        }

        public async Task<Paged<T>> LoadAsync(List<WhereConditionPair> query, int top, int skip)
        {
            var filter = string.Empty;
            if (query != null)
            {
                if (query.Count > 0)
                {
                    filter = "&$filter=";
                }
                foreach (var pair in query)
                {
                    if (pair.Value != null)
                    {
                        switch (pair.Condition)
                        {
                            case WhereCondition.Contains:
                                filter += $"contains({pair.FieldName},'{pair.Value.ToString()}')";
                                break;
                        }
                    }

                }
            }
            
            var res = await httpClient.GetStringAsync(options.LoadUrl + "?$count=true" + filter+"&top="+top+"&skip="+skip);
            return JsonSerializer.Deserialize<Paged<T>>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}