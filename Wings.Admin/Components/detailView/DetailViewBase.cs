using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Wings.Admin.Components.detailView
{
    public class DetailViewBase<TItem> : ComponentBase
    {

        public Dictionary<string, string> PropertyList { get; set; } = new Dictionary<string, string>();
        protected bool render { get; set; } = false;
        [Parameter]
        public TItem Value { get; set; }

        protected override void OnInitialized()
        {
            // first render
            if (!render)
            {
                var propertyList = typeof(TItem).GetProperties();
                Console.WriteLine("propertyList:" + propertyList.Length);
                foreach (var property in propertyList)
                {
                    Console.WriteLine(property.Name);
                    PropertyList.Add(property.Name, property.GetValue(Value)?.ToString());
                }
            }
        }


    }
}