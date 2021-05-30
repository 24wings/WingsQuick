using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Wings.Shared.Dto;

namespace Wings.Admin.Services
{

    public class LocalStorageService
    {
        public Task<List<MenuData>> MenuData { get { return GetMenus(); } set { LocalStorage.SetItemAsync<List<MenuData>>("Menus", value.Result); } }

        public ILocalStorageService LocalStorage { get; }

        private async Task<List<MenuData>> GetMenus()
        {
            return await LocalStorage.GetItemAsync<List<MenuData>>("Menus");
        }

        public LocalStorageService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }
    }
}