using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Wings.Shared.Attributes;

namespace Wings.Admin.Components
{
    public class PropertyComponentBase<TModel> : ComponentBase
    {
        protected bool IsKey{get;set;}
        [Parameter]
        public TModel Value { get; set; } 
        [Parameter]
        public PropertyInfo Property { get; set; }

        protected DisplayAttribute display;
        protected string FieldLabel{get;set;}
        protected FormFieldAttribute formFieldAttribute{get;set;}

         protected override void OnInitialized()
        {
            
            display = Property.GetCustomAttribute<DisplayAttribute>();
            FieldLabel=display==null?Property.Name:display.Name;
            formFieldAttribute=Property.GetCustomAttribute<FormFieldAttribute>();
            IsKey= Property.GetCustomAttribute<KeyAttribute>()==null;
         
        }
    }
}