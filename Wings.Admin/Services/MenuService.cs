using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Shared.Dto;
using System.Text.Json;

namespace Wings.Admin.Services
{

    public class MenuService
    {
        private HttpClient httpClient { get; set; }
        private ConfigService configService { get; set; }
        LocalStorageService localStorageService { get; }


        public MenuService(HttpClient _httpClient, ConfigService _configService, LocalStorageService _localstorageService)
        {
            httpClient = _httpClient;
            configService = _configService;
            localStorageService = _localstorageService;

        }
        public async Task<List<MenuData>> LoadMenus()
        {
            var data = await httpClient.GetAsync(configService.url + "/api/menu/My");
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