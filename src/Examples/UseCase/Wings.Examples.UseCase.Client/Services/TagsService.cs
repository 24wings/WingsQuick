using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Client.Services
{
    public class TagsService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        public TagsService(HttpClient _httpClient,IConfiguration _configuration)
        {
            httpClient = _httpClient;
            _configuration = configuration;

        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public  async Task<bool> DisabledAttrCategory(int id ,bool enable)
        {
         return  await httpClient.GetJsonAsync<bool>(configuration.GetConnectionString("url") + "/api/AttrCategory/ToggleEnable?id=" + id + "&enable=" + enable);
            
        }
    }
}
