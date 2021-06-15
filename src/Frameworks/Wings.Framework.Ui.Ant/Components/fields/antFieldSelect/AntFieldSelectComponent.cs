using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public partial class AntFieldSelectComponent<TModel> : ComponentBase where TModel : ILabel
    {
        [Parameter]
        public TModel Value { get; set; }

        public AntDesign.Select<TModel, TModel> antSelect { get; set; }

        public TModel SelectedItem { get; set; }
        public TModel DefaultSelectedItem { get; set; }
        [Parameter]
        public bool AllowClear { get; set; }


        public DataSourceManager<TModel> DataSource { get; set; }
        public List<TModel> Options { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Value != null) { 
            DefaultSelectedItem = System.Activator.CreateInstance<TModel>();
            DefaultSelectedItem.Id = Value.Id;
            DefaultSelectedItem.Label = Value.Label;
            }



        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                var rtn = await DataSource.Load();
                Options = rtn.Data;
                if (Value != null)
                {
                    DefaultSelectedItem = rtn.Data.Where(item => item.Label == Value.Label).FirstOrDefault();
                    Console.WriteLine("DefaultSelectedItem"+DefaultSelectedItem);
                }

                StateHasChanged();
            }
        }

        protected async Task OnSelectedItemChange()
        {
            await OnValueChange.InvokeAsync(SelectedItem);

        }
    }
}
