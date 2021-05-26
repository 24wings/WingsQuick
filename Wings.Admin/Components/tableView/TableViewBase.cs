using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Wings.Admin.Shared;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Admin.Components.tableView
{

    public class TableViewBase<TModel> : ModelComponentBase<TModel>
    {
        protected Table<TModel> table{get;set;}
        protected EditType editType { get; set; } = EditType.Detail;
        public object EditValue { get; set; }
        protected CRUDModelAttribute CRUDModel { get; set; }
        [Inject]
        public ModalService _modalService { get; set; }
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
                CRUDModel=typeof(TModel).GetCustomAttribute<CRUDModelAttribute>();
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
        public async Task Delete(TModel data)
        {
            await DataSource.Delete(data);
            await Load();
        }
        public void OpenDeleteConfirmModal(TModel data)
        {
            _modalService.Confirm(new ConfirmOptions()
            {
                Title = "确定删除该条记录?",
                OnOk = async (e) =>
                {
                    await DataSource.Delete(data);
                    await Load();
                },
            });
        }
        /// <summary>
        /// 打开新增模态框
        /// </summary>
        public void OpenAddFormModal(){
            editType=EditType.Insert;
            EditValue= Assembly.GetExecutingAssembly().CreateInstance(typeof(TModel).FullName);

        }

        protected string GetPropertyTitle(PropertyInfo item)
        {
            var display = item.GetCustomAttribute<DisplayAttribute>();
            return display?.Name == null ? item.Name : display.Name;

        }
        public RenderFragment dynamicEditComponent => builder =>
              {
                  Type editModelType = null;
                  switch (editType)
                  {
                      case EditType.Detail:
                      case EditType.Insert:
                          editModelType = CRUDModel?.Create==null ?typeof(TModel):CRUDModel.Create;
                          break;
                      case EditType.Update:
                          editModelType = CRUDModel?.Update;
                          break;

                  }

                  editModelType = Assembly.GetExecutingAssembly().DefinedTypes.First(type => type.Name.Contains("DynamicForm") && !type.Name.Contains("Base")).MakeGenericType(editModelType);

                  builder.OpenComponent(0, editModelType);
                  builder.AddAttribute(1, "Value", EditValue);
                  builder.AddAttribute(2, "OnSubmit",
                     EventCallback.Factory.Create<object>(this,
                     RuntimeHelpers.CreateInferredEventCallback(this, async __value =>
                      {
                          EditValue = null;
                          editType=EditType.Detail;
                          await Load();
                      }, new object())));
                  builder.AddAttribute(3, "EditType", editType);

                  builder.CloseComponent();

              };

    }


}