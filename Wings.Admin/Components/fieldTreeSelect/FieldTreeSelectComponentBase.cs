using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Shared;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;
using System.Text.Json;

namespace Wings.Admin.Components.fieldTreeSelect
{
    public class FieldTreeSelectComponentBase<TModel> : PropertyComponentBase<TModel>
    {
        [Inject]
        public ModalService _modalService { get; set; }

        protected Tree<TModel> tree { get; set; }

        protected DataSourceManager<TModel> DataSource { get; set; }

        public bool render { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }


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
        public async Task OnCheckBoxChange()
        {
            var totalCheckDataItemList = new List<TModel>();
            // 查找每个被checked 的父级
            Func<TreeNode<TModel>, List<TModel>> GetAllHalfCheckedParent = null;
            GetAllHalfCheckedParent = node =>
            {
                var result = new List<TModel>();
                var parentNode = node.ParentNode;
                if (parentNode != null && parentNode.Checked != true)
                {
                    result.Add(parentNode.DataItem);
                    if (parentNode.ParentNode != null)
                    {
                        var d = GetAllHalfCheckedParent(parentNode);
                        result.AddRange(d);
                    }

                }
                return result;
            };

            var data = tree.CheckedNodes.AsQueryable().Select(node => (TModel)node.DataItem).Distinct().ToList();
            Console.WriteLine(tree.CheckedNodes.Count);
            tree.CheckedNodes.ForEach(item =>
            {


                var parents = GetAllHalfCheckedParent(item);
                data.AddRange(parents);
            }
                );
            Console.WriteLine("Property.Name:" + Property.Name);
            await OnValueChange.InvokeAsync(data);

        }
    }
}
