using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public abstract class AntTreeViewBase<TModel> : ModelComponentBase<TModel>
    {

        [Inject]
        public ModalService _modalService { get; set; }

        protected Tree<TModel> tree { get; set; }

        protected DataSourceManager<TModel> DataSource { get; set; }

        public bool render { get; set; }


        public List<TModel> DataListTItem { get; set; } = new List<TModel>();


        protected TModel selectedData { get; set; }
        [Inject]
        protected IMapper mapper { get; set; }
        public EditType editType { get; set; }

        protected CrudModelAttribute CRUDModel { get; set; }
        protected object EditValue { get; set; }



        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!render)
            {
                render = true;
                CRUDModel = typeof(TModel).GetCustomAttribute<CrudModelAttribute>();
                StateHasChanged();
            }

        }
        public async Task Load()
        {
            var resData = await DataSource.Load(new BasicQuery { });
            DataListTItem = resData.Data;
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

        public List<TModel> GetChildren(TModel data)
        {
            var children = data.GetType().GetProperty("Children").GetValue(data);
            return data.GetType().GetProperty("Children").GetValue(data) as List<TModel>;
        }
        /// <summary>
        /// 添加同级
        /// </summary>
        public void OpenAddMenuForm()
        {
            editType = EditType.Insert;
            var dest = System.Activator.CreateInstance(CRUDModel.Create);
            EditValue = dest;
            StateHasChanged();
        }

        /// <summary>
        /// 添加下级
        /// </summary>
        public void OpenSubMenuForm()
        {
            editType = EditType.Insert;
            // Console.WriteLine(CRUDModel.Create.FullName);
            var dest = System.Activator.CreateInstance(CRUDModel.Create);  //mapper.Map(selectedData, typeof(TModel), CRUDModel.Create);
            // Console.WriteLine(JsonSerializer.Serialize(System.Activator.CreateInstance(CRUDModel.Create)));
            dest.GetType().GetProperty("ParentId").SetValue(dest, selectedData.GetType().GetProperty("Id").GetValue(selectedData));
            EditValue = dest;
            StateHasChanged();
        }

        public void OpenEditMenuForm()
        {
            editType = EditType.Update;
            var dest = mapper.Map(selectedData, typeof(TModel), CRUDModel.Update);
            EditValue = dest;
            StateHasChanged();
        }

        public void OpenDeleteConfirmModal()
        {
            _modalService.Confirm(new ConfirmOptions()
            {
                Title = "确定删除该条记录?",
                OnOk = async (e) =>
                {
                    await DataSource.Delete(selectedData);
                    await Load();
                },
                OnCancel = (e) => null
            });
        }

        public RenderFragment dynamicEditComponent => builder =>
        {
            Type editModelType = null;
            switch (editType)
            {
                case EditType.Detail:
                case EditType.Insert:
                    editModelType = CRUDModel.Create;
                    break;
                case EditType.Update:
                    editModelType = CRUDModel.Update;
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
                    await Load();



                }, new object())));
            builder.AddAttribute(3, "EditType", editType);

            builder.CloseComponent();

        };

    }


}