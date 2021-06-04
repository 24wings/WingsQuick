using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Ui.Core;
using Wings.Framework.Ui.Core.Services;
using Wings.Framework.Shared;
using Wings.Framework.Ui.Core.Components;
using System;
using System.Linq;

namespace Wings.Framework.Ui.Ant.Shared
{

    public abstract class AntDynamicComponentComboxBase<TModel> : FieldComponentBase<TModel>
    {
        protected List<ComponentPair> ComponentPairs = new List<ComponentPair>();
        protected List<ComponentPair> ComponentPairsDisplay = new List<ComponentPair>();
        protected List<ComponentPair> Options = new List<ComponentPair>();
        protected ComponentPair _selectedItem;
        protected string _selectedValue { get; set; }
        protected override void OnInitialized()
        {
            ComponentPairs = DynamicComponentScanner.ComponentPairs;
            ComponentPairsDisplay = ComponentPairs.ToList();
        }

        protected void OnSelectedItemChangedHandler(ComponentPair value)
        {
            _selectedItem = value;
            Console.WriteLine($"selected: ${value?.ComponentDisplayName}");
        }
        protected void OnSearch(string value)
        {
            ComponentPairsDisplay = ComponentPairs.Where(pair => pair.ComponentDisplayName.Contains(value) || pair.ComponentFullName.Contains(value)).ToList();
            Console.WriteLine($"search: {value}");
        }
    }
}