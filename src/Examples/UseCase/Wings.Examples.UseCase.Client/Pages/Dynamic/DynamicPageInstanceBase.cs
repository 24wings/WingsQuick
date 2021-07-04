using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos.Admin;

namespace Wings.Examples.UseCase.Client.Pages
{
    public partial class DynamicPageInstance:ComponentBase
    {
        [Parameter]
        public PageData PageData { get; set; }
  


        
    }
}
