using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Wings.Admin;
using Wings.Shared.Dvo;

namespace Wings.Admin.Components.fieldDateRange
{
    public class FieldDateRangeBase : ComponentBase
    {
        [Parameter]
        public object FieldValue { get; set; }

        protected DisplayAttribute display;

        public DateRange Value { get; set; }
        public DateTime?[] RangeValue { get; set; } = new DateTime?[] { };
        [Parameter]
        public PropertyInfo Prop { get; set; }
        [Parameter]
        public EventCallback<object> OnValueChange { get; set; }
        protected void changeValue(DateRangeChangedEventArgs args)
        {

            if (args.Dates.Length > 0)
            {
                Value = new DateRange(args.Dates[0].Value, args.Dates[1].Value);
                OnValueChange.InvokeAsync(Value);

            }
            else
            {
                Value = null;
                OnValueChange.InvokeAsync(null);
            }

        }

        protected override void OnInitialized()
        {

            display = Prop.GetCustomAttribute<DisplayAttribute>();
        }
    }
}