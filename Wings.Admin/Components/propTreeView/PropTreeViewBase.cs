using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Components;
using Wings.Admin.Shared;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;
using System.Text.Json;
using System.Linq;

namespace Wings.Admin.Components.propTreeView
{
    public class PropTreeViewBase<TModel> : PropertyComponentBase<TModel>
    {
        [Inject]
        public ModalService _modalService { get; set; }

        protected Tree<object> tree { get; set; }


        public bool render { get; set; }


        public List<object> DataListTItem { get; set; } = new List<object>();



        [Inject]
        protected IMapper mapper { get; set; }
        public EditType editType { get; set; }
        public Type PropertyGenericType { get; set; }

        protected CRUDModelAttribute CRUDModel { get; set; }
        protected object EditValue { get; set; }
        protected async override Task OnInitializedAsync()
        {
            base.OnInitialized();
            if (!render)
            {
                render = true;
                PropertyGenericType = Property.PropertyType.GenericTypeArguments[0];
                Console.WriteLine("Value:" + JsonSerializer.Serialize(Value));

                DataListTItem = JsonSerializer.Deserialize<List<object>>(JsonSerializer.Serialize(Property.GetValue(Value)));

                Console.WriteLine("DataListTItem:" + JsonSerializer.Serialize(DataListTItem));

                StateHasChanged();
            }

        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
            }

        }
        public string GetTitle(object data)
        {
            var originData = JsonSerializer.Deserialize(JsonSerializer.Serialize(data), PropertyGenericType);
            return originData.GetType().GetProperty("Title").GetValue(originData).ToString();

        }

        /// <summary>
        /// 这部分写的不好 要重新再写
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<object> GetChildren(object data)
        {
            var originData = ParseData(data);
           
            var Children= JsonSerializer.Deserialize<List<object>>( JsonSerializer.Serialize(originData.GetType().GetProperty("Children").GetValue(originData)) );
            return Children;
        }

        private object ParseData(object data)
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(data), PropertyGenericType);
        }
    }
}