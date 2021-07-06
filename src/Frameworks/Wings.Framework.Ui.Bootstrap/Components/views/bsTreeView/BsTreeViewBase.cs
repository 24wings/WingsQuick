using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Bootstrap.Components
{
    public abstract class BsTreeViewBase<TModel> : Wings.Framework.Ui.Core.Components.TreeView<TModel>
    {
        protected DataSourceManager<TModel> DataSource { get; set; }
        protected List<TreeItem> Items { get; set; } = new List<TreeItem>();

        public async Task Load()
        {

            var rtn = await DataSource.Load();

            var topTree = rtn.Data.Where(item => item.GetType().GetProperty("ParentId").GetValue(item) == null).FirstOrDefault();
            Items = new List<TreeItem> { new TreeItem { Text = typeof(TModel).GetProperty("Title").GetValue(topTree).ToString() } };
            StateHasChanged();
            var children = rtn.Data.Where(item => (int)item.GetType().GetProperty("ParentId").GetValue(item) == (int)topTree.GetType().GetProperty("Id").GetValue(topTree)).ToList();
            System.Console.WriteLine("children count:" + children.Count);
            foreach (var child in children)
            {
                Items[0].AddItem(new TreeItem { Text = typeof(TModel).GetProperty("Title").GetValue(child).ToString() });
            }

        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.OnAfterRenderAsync(firstRender);
                await Load();
            }

        }

    }
}