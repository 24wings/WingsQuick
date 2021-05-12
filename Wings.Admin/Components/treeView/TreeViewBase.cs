using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;
using Wings.Shared.Dvo;
using Wings.Shared.Attributes;
using Wings.Admin.Components.dynamicForm;
using Wings.Admin.Shared;
using Microsoft.AspNetCore.Components.CompilerServices;
using AntDesign;


namespace Wings.Admin.Components
{


    public class TreeViewBase<TModel> : ModelComponentBase<TModel>
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

        protected CRUDModelAttribute CRUDModel { get; set; }
        protected object EditValue { get; set; }



        protected async override Task OnInitializedAsync()
        {
            base.OnInitialized();
            if (!render)
            {
                render = true;
                CRUDModel = typeof(TModel).GetCustomAttribute<CRUDModelAttribute>();


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
            var dest = mapper.Map(selectedData, typeof(TModel), CRUDModel.Create);
            EditValue = dest;
            StateHasChanged();
        }

        /// <summary>
        /// 添加下级
        /// </summary>
        public void OpenSubMenuForm()
        {
            editType = EditType.Insert;
            var dest = mapper.Map(selectedData, typeof(TModel), CRUDModel.Create);
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
                Title = "Do you Want to delete these items?",
                Content = "Some descriptions",
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