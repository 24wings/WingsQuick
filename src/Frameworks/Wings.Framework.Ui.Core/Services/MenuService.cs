using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Framework.Shared.Dtos;

namespace Wings.Framework.Ui.Core.Services
{
    public class ConfigService
    {

        private IConfiguration Configuration { get; }
        public ConfigService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string url { get { return Configuration.GetConnectionString("url"); } }

    }
    public class MenuService
    {
        private readonly HttpClient httpClient;
        private ConfigService configService { get; set; }
        LocalStorageService localStorageService { get; }
       private  NavigationManager navigationManager;
        readonly ILocalStorageService local;

        public MenuService(HttpClient _httpClient, ConfigService _configService, LocalStorageService _localstorageService,ILocalStorageService _local, NavigationManager _navigationManager)
        {
            httpClient = _httpClient;
            configService = _configService;
            localStorageService = _localstorageService;
            local = _local;
            navigationManager = _navigationManager;

        }
        public async Task<List<MenuData>> LoadMenus()
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("Get"),
                RequestUri = new Uri(navigationManager.BaseUri+"api/menu/My"),
               
            };
           var authToken= await local.GetItemAsStringAsync("authToken");
            if (authToken == null)
            {
                
                if (navigationManager.Uri != "/Login")
                {
                    navigationManager.NavigateTo("/Login");
                    return null;
                }


            }
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"",""));
           var data= await httpClient.SendAsync(requestMessage);
             
            var str = await data.Content.ReadAsStringAsync();
            var rtn = JsonSerializer.Deserialize<List<MenuData>>(str, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            localStorageService.MenuData = Task.FromResult(rtn);
            return rtn;
        }
        /// <summary>
        /// 查找每个被链接指向菜单
        /// </summary>
        /// <returns></returns>
        public async Task<MenuData> GetMenuDataByLink(string link)
        {
            link = new Uri(link).PathAndQuery;
            Console.WriteLine("l:" + link);
            MenuData menuData = null;
            Func<MenuData, MenuData> FindLinkNode = null;
            FindLinkNode = (node) =>
     {
         if (node != null && node.Link == link)
         {
             Console.WriteLine("link:" + link + JsonSerializer.Serialize(node));

             menuData = node;
         }
         else
         {
             foreach (var child in node.Children)
             {
                 FindLinkNode(child);
             }
         }
         Console.WriteLine("menuData2:" + menuData);
         return menuData;
     };
            var menuDataList = await localStorageService.MenuData;
            var result = FindLinkNode(menuDataList[0]);
            Console.WriteLine("find result link node" + result);
            return result;

        }

        /// <summary>
        /// 根据链接查找地址
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task<List<MenuData>> GetMenuDataSegmentsByLink(MenuData menuData)
        {

            var menuDataList = await localStorageService.MenuData;
            List<MenuData> result = new List<MenuData> { menuData };
            var data = await httpClient.GetAsync(configService.url + "/api/menu/LoadMenuSegmentsByMenuId?id=" + menuData.Id);
            var rtn = JsonSerializer.Deserialize<MenuData>(await data.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            while (rtn.Parent != null)
            {
                result.Insert(0, rtn.Parent);
                rtn = rtn.Parent;

            }
            return result;

        }

    }
}
