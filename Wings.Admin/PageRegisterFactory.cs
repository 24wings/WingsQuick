using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Shared;
using Wings.Shared.Attributes;

namespace Wings.Admin
{
    /// <summary>
    /// 页面注册器
    /// </summary>
    public static class PageRegisterFactory
    {
        private static Dictionary<Type, ComponentBase> registedComponents = new Dictionary<Type, ComponentBase>();
        private static Dictionary<Type, Type> registedPageComponents = new Dictionary<Type, Type>(){
            {typeof(TreePageAttribute),typeof(DynamicTreeView)},
            {typeof(TablePageAttribute),typeof(DynamicTableView)
            },
            };
        public static Type GetPageDefaultComponent(Type type)
        {

            return registedPageComponents[type];

        }
    }
}