using System.Collections.Generic;
using Wings.Admin.Components.fieldString;
using Wings.Admin.Components.fieldNumber;
using Microsoft.AspNetCore.Components;
using Wings.Admin.Components.fieldDate;
using Wings.Admin.Components.fieldDateRange;
using Wings.Admin.Components.fieldCheckbox;
using Wings.Admin.Components.fieldEnum;
using Wings.Shared;
using System;
using Wings.Shared.Dvo;
using Wings.Admin.Components.propString;
using System.Reflection;
using Wings.Shared.Attributes;
using Wings.Admin.Components.propTreeView;
using Wings.Admin.Components.fieldTreeSelect;

namespace Wings.Admin
{
    public static class ComponentRegisterFactory
    {
        private static Dictionary<Type, ComponentBase> registedComponents = new Dictionary<Type, ComponentBase>();
        private static Dictionary<Type, Type> registedFieldComponents = new Dictionary<Type, Type>(){
            {typeof(String),typeof(FieldString<object>) },
            {typeof(Int32),typeof(FieldNumber<object>)},
            {typeof(DateTime),typeof(FieldDate<object>)},
            {typeof(DateRange),typeof(FieldDateRange)},
            {typeof(Boolean),typeof(FieldCheckbox)},
            {typeof(Enum),typeof(FieldEnum)},
            {typeof(TreeSelectFieldAttribute),typeof(FieldTreeSelect<object>)}

            };

        private static Dictionary<Type, Type> registePropComponents = new Dictionary<Type, Type>(){
            {typeof(String),typeof(PropString<object>) },
            {typeof(Int32),typeof(PropString<object>)},
            {typeof(TreePageAttribute),typeof(PropTreeView<object>)},
            {typeof(DateRange),typeof(FieldDateRange)},
            {typeof(Boolean),typeof(FieldCheckbox)},
            {typeof(Enum),typeof(FieldEnum)}

            };

        public static void RegistComponent(Type type, ComponentBase com)
        {
            registedComponents.Add(type, com);
        }

        public static Type GetFieldDefaultComponent(Type type)
        {
            
            if (type.IsEnum)
            {
                return registedFieldComponents[typeof(Enum)];
            }
           
            return registedFieldComponents[type];

        }

        public static Type GetPropDefaultComponent(Type type)
        {
        // 列表
            if(type.IsGenericType){
                var genericArgument= type.GetGenericArguments()[0];
             var pageAttribute=   genericArgument.GetCustomAttribute<PageAttribute>();
            return  registePropComponents[pageAttribute.GetType()];
            }

            if (type.IsEnum)
            {
                return registePropComponents[typeof(Enum)];
            }

            var defaultType = registePropComponents.GetValueOrDefault(type);
            if (defaultType == null)
            {
                return typeof(PropString<object>);
            }
            return defaultType;

        }



    }

}