using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Client.Shared
{
    public class NavMenuItemBase : ComponentBase
    {
        [Parameter]
        public MenuData menuData { get; set; }

        [Parameter] 
        public List<MenuData> MenuDataList { get; set; }


        protected List<MenuData> GetMenuChildren()
        {
            return MenuDataList.Where(menu => menu.ParentId == menuData.Id).ToList();
        }

    }
}