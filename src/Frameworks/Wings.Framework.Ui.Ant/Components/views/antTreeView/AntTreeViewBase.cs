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
using Newtonsoft.Json;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public partial class AntTreeView<TModel> : TreeView<TModel> 
        where TModel : BasicTree<TModel>
    {
        [Parameter]
        public List<WhereConditionPair> InitFilter { get; set; } = new List<WhereConditionPair>();

        [Inject]
        public ModalService _modalService { get; set; }

        protected Tree<TModel> tree { get; set; }

        protected DataSourceManager<TModel> DataSource { get; set; }

        [Parameter]
        public List<TModel> CheckedNodes { get; set; }
        [Parameter]
        public EventCallback<List<TModel>> CheckedNodesChanged { get; set; }
        [Parameter]
        public int[] CheckedKeys { get; set; } = new List<int>().ToArray();
        /// <summary>
        /// 复选框
        /// </summary>
        [Parameter]
        public bool Checkable { get; set; } = false;

        public bool render { get; set; }

        public List<TModel> TopTItems { get; set; } = new List<TModel>();

        public List<TreeNode<TModel>> TreeNodes { get; set; }

        [Parameter]
        public List<TModel> DataListTItem { get; set; }

        [Parameter]
        public TModel SelectedData { get; set; }

        [Inject]
        protected IMapper mapper { get; set; }


        protected object EditValue { get; set; }

        protected void GetChildren(TModel model)
        {
            if (DataListTItem.Any(item => item.ParentId == model.Id))
            {
                var children = DataListTItem.Where(item => item.ParentId == model.Id).ToList();
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
            data = data.Select(item=> JsonConvert.DeserializeObject<TModel>(JsonConvert.SerializeObject(item))).ToList();
            data.ForEach(item => item.Children = null);

            await CheckedNodesChanged.InvokeAsync(data); 

        }






        protected override void OnInitialized()
        {
            if (CheckedNodes != null)
            {
                CheckedKeys = CheckedNodes.Select(node => (int)node.GetType().GetProperty("Id").GetValue(node)).ToArray();
            }
            base.OnInitialized();
            if (!render)
            {
                render = true;
                StateHasChanged();
            }

        }
        public async Task Load()
        {
            if (DataListTItem == null)
            {
                var resData = await DataSource.Load();
                DataListTItem = resData.Data;

            }
            TopTItems = DataListTItem.Where(item => (int?)item.GetType().GetProperty("ParentId").GetValue(item) == null|| (int?)item.GetType().GetProperty("ParentId").GetValue(item)==0).ToList();
            TreeNodes = DataListTItem.Select(item => new TreeNode<TModel>() { DataItem = item }).ToList();
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
                if (CheckedKeys.Count() > 0)
                {
                    tree.ChildNodes.ForEach(node => EnumChildren(node));

                }
                StateHasChanged();

                tree.ExpandAll();

            }

        }
        public void EnumChildren(TreeNode<TModel> treeNode)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                treeNode.ChildNodes.ForEach(child => EnumChildren(child));
            }
            else
            {
                var dataItemId = (int)treeNode.DataItem.GetType().GetProperty("Id").GetValue(treeNode.DataItem);

                if (CheckedKeys.Contains(dataItemId))
                {
                    Console.WriteLine("find checked key:" + dataItemId);
                    treeNode.SetChecked(true);
                    treeNode.Checked = true;
                    tree.CheckedNodes.Add(treeNode);
                    

                }
                else
                {
                }
            }

        }
        public async Task Refresh()
        {
            DataListTItem = null;
            await Load();
        }










    }


}