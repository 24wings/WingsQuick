using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Wings.Admin.Shared;
using Wings.Shared.Dto;

namespace Wings.Admin.Components.tableView
{

    public class TableViewBase<TModel> : ModelComponentBase<TModel>
    {
        protected DataSourceManager<TModel> DataSource { get; set; }
        protected bool render { get; set; } = false;
        protected int PageIndex = 0;
        protected int PageSize = 10;
        protected int Total = 0;
        public List<TModel> Data { get; set; } = new List<TModel>();
        protected List<TModel> SelectedRows { get; set; } = new List<TModel>();
        protected List<PropertyInfo> PropList { get; set; } = new List<PropertyInfo>();
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!render)
            {
                PropList = new List<PropertyInfo>(typeof(TModel).GetProperties());
                render = true;
            }


        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Load();
            }

        }

        public async Task Load()
        {
            var resData = await DataSource.Load(new BasicQuery { PageIndex = PageIndex, PageSize = PageSize });
            (Data, Total) = (resData.Data, resData.Total);
            StateHasChanged();

        }
    }
}