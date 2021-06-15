using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Dtos;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntFieldDateRangeBase<TModel> : FieldComponentBase<TModel>
    {
        [Parameter]
        public object FieldValue { get; set; }


        public DateTime?[] RangeValue { get; set; } = new DateTime?[] { };
        [Parameter]
        public PropertyInfo Prop { get; set; }
    
        protected void changeValue(DateRangeChangedEventArgs args)
        {

            // if (args.Dates.Length > 0)
            // {
            //     Value = new DateRange(args.Dates[0].Value, args.Dates[1].Value);
            //     OnValueChange.InvokeAsync(Value);

            // }
            // else
            // {
            //     Value = null;
            //     OnValueChange.InvokeAsync(null);
            // }

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

        }
    }
}