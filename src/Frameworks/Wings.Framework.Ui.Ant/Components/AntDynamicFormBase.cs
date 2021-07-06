using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntDynamicFormBase<TModel> : ModelComponentBase<TModel>
    {
        [Parameter]
        public int DefaultFieldSpan { get; set; } = 6;

        /// <summary>
        /// such value as : inline,modal
        /// </summary>
        [Parameter]
        public string mode { get; set; } = "inline";
        [Parameter]
        public bool Visible { get; set; }
        [Parameter]
        public EditType editType { get; set; }
        public List<PropertyInfo> props = new List<PropertyInfo>();
        protected EditContext editContext { get; set; }

        public DataSourceManager<TModel> DataSource;
        protected string ValueString { get; set; }


        protected bool _visible = true;

        public List<AntDynamicField<TModel>> Fields { get; set; } = new List<AntDynamicField<TModel>>();
        protected AntDynamicField<TModel> childFieldComponent
        {
            set => Fields.Add(value);
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
            }
        }

        private void CallChildMethod(int index, int value)
        {
            Fields.ElementAt(index);
        }

        protected bool render { get; set; } = false;
        [Parameter]
        public EventCallback<TModel> OnSubmit { get; set; }
        public void ShowModal(TModel item)
        {

            Value = item;
            mode = "modal";
            Visible = true;

        }
        public async Task InsertAsync()
        {
            await DataSource.Insert(Value);
        }
        public async Task UpdateAsync()
        {
            await DataSource.Update(Value);
        }

        protected override void OnInitialized()
        {
            Console.WriteLine("Value:" + Value);
            base.OnInitialized();
            if (!render)
            {
                if (Value == null)
                {
                    Value = (TModel)typeof(TModel).Assembly.CreateInstance(typeof(TModel).FullName);
                }


                editContext = new EditContext(Value);
                LoadPropertitys();
                render = true;
            }
        }
        protected void LoadPropertitys()
        {
          
            
                props = typeof(TModel).GetProperties()
               .Where(prop => prop.GetCustomAttribute<IgnoreFieldAttribute>() == null&&prop.GetCustomAttribute<IgnoreAttribute>()==null)
               .ToList();
            
            
            
        }
        /// <summary>
        /// 重置表单
        /// </summary>
        public void ResetForm()
        {
            Value = (TModel)System.Activator.CreateInstance(typeof(TModel));
            editContext = new EditContext(Value);
            

        }


        protected void changeValue(PropertyInfo prop, object value)
        {
            var field = editContext.Field(prop.Name);
            Console.WriteLine("fieldModel:" + JsonSerializer.Serialize(field.Model));
            Console.WriteLine("值变更:" + prop.Name + "  value:" + value);
            editContext.NotifyFieldChanged(field);
            editContext.NotifyValidationStateChanged();
            ValueString = JsonSerializer.Serialize(field.Model);
            StateHasChanged();

        }

        protected async Task HandleOk(MouseEventArgs e)
        {
            if (editContext.Validate())
            {


                await OnSubmit.InvokeAsync(Value);
                Visible = false;

            }
            else
            {

                Console.WriteLine(e);
            }


        }

        protected async Task HandleCancel(MouseEventArgs e)
        {
            Visible = false;
        }

        protected object GetFieldValue(PropertyInfo item)
        {
            var value = typeof(TModel).GetProperty(item.Name).GetValue(Value);
            return typeof(TModel).GetProperty(item.Name).GetValue(Value);
        }
        public string GetTitle()
        {
            switch (editType)
            {
                case EditType.Detail:
                    return "详情";
                case EditType.Insert:
                    return "新增";
                default:
                    return "更新";

            }
        }
    }
}
