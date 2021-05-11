using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
namespace Wings.Admin.Components.dynamicForm
{

    public class DynamicFormBase : ComponentBase
    {
        public List<PropertyInfo> props = new List<PropertyInfo>();
        protected EditContext editContext { get; set; }

        [Parameter]
        public Type type { get; set; }
        private string name;
        protected object obj { get; set; }
        protected bool render { get; set; } = false;
        [Parameter]
        public object Value { get; set; }

        protected override void OnInitialized()
        {
            if (!render)
            {
                if (Value == null)
                {
                    obj = type.Assembly.CreateInstance(type.FullName);
                }
                else
                {
                    obj = Value;
                }

                editContext = new EditContext(obj);
                props = type.GetProperties().ToList();
                render = true;
            }
        }

        public string result { get; set; }
        protected void ParseData()
        {
            result = JsonSerializer.Serialize(obj);
            StateHasChanged();
        }


        private void HandleSubmit()
        {
            var isValid = editContext.Validate();
        }
        protected void changeValue(PropertyInfo prop, object value)
        {
            Type objType = obj.GetType();
            objType.GetProperty(prop.Name).SetValue(obj, value);
            var filed = editContext.Field(prop.Name);
            Console.WriteLine("值变更:" + prop.Name + "  value:" + value);
            editContext.NotifyFieldChanged(filed);
            editContext.NotifyValidationStateChanged();

        }
    }
}