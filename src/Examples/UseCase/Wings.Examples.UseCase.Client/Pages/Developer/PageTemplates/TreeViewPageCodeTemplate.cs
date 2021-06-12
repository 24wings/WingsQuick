using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Client.Pages
{
    public partial class TreeViewPageCodeTemplate<TListModel, TCreateModel, TUpdateModel>:ComponentBase
    {
        [Parameter]
        public CodeGeneratorBase CodeConfig { get; set; }
        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        public async Task CopyCode(string id)
        {
           await jSRuntime.InvokeVoidAsync("clipboardCopy.copyText", new object[] { "client-razor-code" });
        }
    }
}
