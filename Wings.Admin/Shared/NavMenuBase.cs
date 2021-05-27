using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Services;
using Wings.Shared.Dto;

namespace Wings.Admin.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        protected MenuService menuService { get; set; }

        protected List<MenuData> MenuDataList { get; set; } = new List<MenuData>();
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            MenuDataList = await menuService.LoadMenus();

        }

    }
}