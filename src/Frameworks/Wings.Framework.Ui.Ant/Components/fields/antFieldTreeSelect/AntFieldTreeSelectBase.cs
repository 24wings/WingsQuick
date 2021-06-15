using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using AntDesign;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.CompilerServices;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Ui.Core.Components;
using Wings.Framework.Shared.Dtos;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntFieldTreeSelectBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }
        [Inject]
        protected HttpClient httpClient { get; set; }
        [Inject]
        protected IConfiguration configuration { get; set; }
  
        protected Type DataType { get; set; }
        public List<object> DataList { get; set; } = new List<object>();
        protected string[] selectedKeys { get; set; }


        protected override void OnInitialized()
        {
            DataType = Property.PropertyType.GenericTypeArguments[0];
            base.OnInitialized();


        }
        protected async override Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
            await Load();


        }
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                StateHasChanged();

            }
        }

        public async Task Load()
        {
            var treeSelectField = Property.GetCustomAttribute<TreeSelectFieldAttribute>();

            var res = await httpClient.GetAsync(configuration.GetConnectionString("url") + treeSelectField.Url);
            var peropertyResultDataType = typeof(BasicQueryResult<object>).GetGenericTypeDefinition().MakeGenericType(DataType);
            var data = JsonSerializer.Deserialize(await res.Content.ReadAsStringAsync(), peropertyResultDataType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            DataList = JsonSerializer.Deserialize<List<object>>(JsonSerializer.Serialize(data.GetType().GetProperty("Data").GetValue(data)));
            Console.WriteLine("DataList:" + JsonSerializer.Serialize(DataList));
        }
        protected RenderFragment dynamicTreeComponent => builder =>
      {


          builder.OpenComponent(0, typeof(AntFieldTreeSelectComponent<object>).GetGenericTypeDefinition().MakeGenericType(DataType));
          //   builder.AddAttribute(1, "Value", Property.GetValue(Value));
          builder.AddAttribute(2, "Property", Property);
          builder.AddAttribute(3, "OnValueChange",
  EventCallback.Factory.Create<object>(this,
  RuntimeHelpers.CreateInferredEventCallback(this, __value =>
  {

      FieldValue =
  __value;

      var __valueCase = JsonSerializer.Deserialize(JsonSerializer.Serialize(__value), Property.PropertyType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
      Property.SetValue(Value, __valueCase);
      OnValueChange.InvokeAsync(FieldValue);
  }, FieldValue)));
          builder.CloseComponent();

      };
        /// <summary>
        /// 这部分写的不好 要重新再写
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<object> GetChildren(object data)
        {
            var originData = ParseData(data);

            var Children = JsonSerializer.Deserialize<List<object>>(JsonSerializer.Serialize(originData.GetType().GetProperty("Children").GetValue(originData)));
            return Children;
        }

        private object ParseData(object data)
        {
            return JsonSerializer.Deserialize(JsonSerializer.Serialize(data), DataType);
        }

        public string GetTitle(object data)
        {
            var originData = JsonSerializer.Deserialize(JsonSerializer.Serialize(data), DataType);
            return GetKey(data) + ":" + originData.GetType().GetProperty("Title").GetValue(originData).ToString();

        }
        public string GetKey(object data)
        {
            var originData = JsonSerializer.Deserialize(JsonSerializer.Serialize(data), DataType, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
            return originData.GetType().GetProperty("Id").GetValue(originData).ToString();

        }
        protected bool IsLeaf(object data)
        {
            var originData = JsonSerializer.Deserialize(JsonSerializer.Serialize(data), DataType, new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
            var children = originData.GetType().GetProperty("Children").GetValue(originData);
            return JsonSerializer.Deserialize<List<object>>(JsonSerializer.Serialize(children)).Count == 0;
        }
        public void Log()
        {
        }
    }


}