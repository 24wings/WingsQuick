using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Dtos;
using Wings.Shared.Dto;

namespace Wings.Examples.UseCase.Client
{
    public class NavMenuItemBase : ComponentBase
    {
        [Parameter]
        public MenuData menuData { get; set; }

    }
}