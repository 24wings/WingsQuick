using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Linq;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Ui.Core.Components;
using Wings.Framework.Shared.Dtos;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntPropTreeViewBase<TModel> : PropertyComponentBase<TModel>
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

        protected CrudModelAttribute CRUDModel { get; set; }
        protected object EditValue { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!render)
            {
                render = true;
                PropertyGenericType = Property.PropertyType.GenericTypeArguments[0];

                DataListTItem = JsonSerializer.Deserialize<List<object>>(JsonSerializer.Serialize(Property.GetValue(Value)));


                StateHasChanged();
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
            var id = (int)originData.GetType().GetProperty("Id").GetValue(originData);

            return DataListTItem.Where(item => ((int?)ParseData(item).GetType().GetProperty("ParentId").GetValue(ParseData(item))) == id).ToList();


        }

        private object ParseData(object data)
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(data), PropertyGenericType, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
        }
    }
}