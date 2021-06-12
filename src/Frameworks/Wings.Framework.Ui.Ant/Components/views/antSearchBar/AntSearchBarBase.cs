using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Ui.Core.Components;

namespace Wings.Framework.Ui.Ant.Components
{
    public abstract class AntSearchBarBase<TModel> : ModelComponentBase<TModel>
    {

        public AntDynamicForm<TModel> dynamicForm { get; set; }

        [Parameter]
        public DataSourceManager<TModel> dataSourceManager { get; set; }
        [Parameter]
        public EventCallback<List<WhereConditionPair>> OnSearch { get; set; }
        public async Task OnSearchButtonClick(TModel value)
        {
            var whereConditionPair = typeof(TModel).GetProperties().Where(prop => prop.GetCustomAttribute<WhereAttribute>() != null && prop.GetValue(value) != null).Select(prop =>
                {
                    var whereAttribute = prop.GetCustomAttribute<WhereAttribute>();
                    return new WhereConditionPair
                    {
                        Condition = whereAttribute.whereCondition,
                        FieldName = whereAttribute.FieldName == null ? prop.Name : whereAttribute.FieldName,
                        Value = prop.GetValue(value)

                    };
                }
            ).ToList();
            await OnSearch.InvokeAsync(whereConditionPair);
     
        }
        public void OnResetButtonClick()
        {
            dynamicForm.ResetForm();
        }

    }
}