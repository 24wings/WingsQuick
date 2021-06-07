using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public partial class AntTreeView<TModel> : TreeViewBase<TModel> where TModel :BasicTree<TModel>
    {

        [Inject]
        public ModalService _modalService { get; set; }

        protected Tree<TModel> tree { get; set; }

        protected DataSourceManager<TModel> DataSource { get; set; }

        public bool render { get; set; }

        public List<TModel> TopTItems { get; set; } = new List<TModel>();

        public List<TModel> DataListTItem { get; set; } = new List<TModel>();

        protected TModel selectedData { get; set; }
        [Parameter]
        public TModel SelectedData { get; set; }

        [Inject]
        protected IMapper mapper { get; set; }
        public EditType editType { get; set; }


        protected object EditValue { get; set; }

        protected  void GetChildren(TModel model){
            if (DataListTItem.Any(item => item.ParentId == model.Id))
            {
               var children= DataListTItem.Where(item => item.ParentId == model.Id).ToList();
                model.Children = children;
                model.Children.ForEach(child => GetChildren(child));
            }
            else
            {
                model.Children = new List<TModel>();

            }
            }

        [Parameter]
        public EventCallback<TModel> SelectedDataChanged { get; set; }

        protected async Task ChangeSelectedData(TModel value)
        {
            await SelectedDataChanged.InvokeAsync(value);
            SelectedData = value;

        }

    



        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!render)
            {
                render = true;
                //CRUDModel = typeof(TModel).GetCustomAttribute<CrudModelAttribute>();
                StateHasChanged();
            }

        }
        public async Task Load()
        {
            var resData = await DataSource.Load();
            DataListTItem = resData.Data;
            TopTItems = resData.Data.Where(item => (int?)item.GetType().GetProperty("ParentId").GetValue(item) == null).ToList();
            TopTItems.ForEach(top => GetChildren(top));
            StateHasChanged();

            tree.ExpandAll();
            StateHasChanged();
        }
     
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Load();
            }

        }
        public async Task Refresh()
        {
            await Load();
        }


        // public List<TModel> GetChildren(TModel data)
        // {
        //     var children = data.GetType().GetProperty("Children").GetValue(data);
        //     return data.GetType().GetProperty("Children").GetValue(data) as List<TModel>;
        // }
        /// <summary>
        /// 这部分写的不好 要重新再写
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public List<TModel> GetChildren(TModel data)
        //{
        //    // var originData = ParseData(data);
        //    return (List<TModel>)data.GetType().GetProperty("Children").GetValue(data) as List<TModel>;



        //}
        public bool IsLeaf(TModel data)
        {
            return DataListTItem.Any(item => (item.GetType().GetProperty("ParentId").GetValue(item) as int?) == (int)item.GetType().GetProperty("Id").GetValue(data));
        }
        /// <summary>
        /// 添加同级
        /// </summary>
        //public void OpenAddMenuForm()
        //{
        //    editType = EditType.Insert;
        //    var dest = System.Activator.CreateInstance(CRUDModel.Create);
        //    EditValue = dest;
        //    StateHasChanged();
        //}

        /// <summary>
        /// 添加下级
        /// </summary>
        //public void OpenSubMenuForm()
        //{
        //    editType = EditType.Insert;
        //    var dest = System.Activator.CreateInstance(CRUDModel.Create);  //mapper.Map(selectedData, typeof(TModel), CRUDModel.Create);
        //    dest.GetType().GetProperty("ParentId").SetValue(dest, SelectedData.GetType().GetProperty("Id").GetValue(SelectedData));
        //    EditValue = dest;
        //    StateHasChanged();
        //}

        //public void OpenEditMenuForm()
        //{
        //    editType = EditType.Update;
        //    var dest = mapper.Map(SelectedData, typeof(TModel), CRUDModel.Update);
        //    EditValue = dest;
        //    StateHasChanged();
        //}

        //public void OpenDeleteConfirmModal()
        //{
        //    _modalService.Confirm(new ConfirmOptions()
        //    {
        //        Title = "确定删除该条记录?",
        //        OnOk = async (e) =>
        //        {
        //            await DataSource.Delete(SelectedData);
        //            await Load();
        //        },
        //        OnCancel = (e) => null
        //    });
        //}

        //public RenderFragment dynamicEditComponent => builder =>
        //{
        //    Type editModelType = null;
        //    switch (editType)
        //    {
        //        case EditType.Detail:
        //        case EditType.Insert:
        //            editModelType = CRUDModel.Create;
        //            break;
        //        case EditType.Update:
        //            editModelType = CRUDModel.Update;
        //            break;

        //    }

        //    editModelType = typeof(AntDynamicForm<>).MakeGenericType(editModelType);

        //    builder.OpenComponent(0, editModelType);
        //    builder.AddAttribute(1, "Value", EditValue);
        //    builder.AddAttribute(2, "OnSubmit",
        //       EventCallback.Factory.Create(this,
        //       RuntimeHelpers.CreateInferredEventCallback(this, async __value =>
        //        {
        //            EditValue = null;
        //            await Load();



        //        }, new object())));
        //    builder.AddAttribute(3, "EditType", editType);

        //    builder.CloseComponent();

        //};
        private object ParseData(object data)
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(data), typeof(TModel), new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
        }


        

    }


}