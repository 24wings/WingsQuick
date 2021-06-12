using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Services;
using System.Text.Json;
using Wings.Examples.UseCase.Client.Services;

namespace Wings.Examples.UseCase.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        public bool collapsed = false;



        protected void ToggleCollapsed()
        {
            collapsed = !collapsed;
        }
        [Inject]
        protected MenuService menuService { get; set; }
        [Inject]
        protected IAuthService authService { get; set; }
        [Parameter]
        public List<MenuData> MenuDataList { get; set; } = new List<MenuData>();
        protected List<MenuData> TopMenu { get; set; } = new List<MenuData>();
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
          
                if (MenuDataList != null)
                {
                    TopMenu = MenuDataList.Where(menu => menu.ParentId == 0 || menu.ParentId == null).ToList();

                }

        }

        
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (MenuDataList != null)
                {
                    TopMenu = MenuDataList.Where(menu => menu.ParentId == 0 || menu.ParentId == null).ToList();

                }
            }
        }
    }
}