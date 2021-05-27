using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Wings.Shared.Dto;

namespace Wings.Admin.Shared
{
    public class NavMenuItemBase : ComponentBase
    {
        [Parameter]
        public MenuData menuData { get; set; }

    }
}