using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Services;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntBreadcrumbBase : ComponentBase
    {
        [Inject]
        public MenuService menuService { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }

        protected List<MenuData> Segements { get; set; } = new List<MenuData>();
        public AntBreadcrumbBase()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            var menu = await menuService.GetMenuDataByLink(navigationManager.Uri);
            if (menu != null)
            {
                Segements = await menuService.GetMenuDataSegmentsByLink(menu);
            }

        }
    }

}