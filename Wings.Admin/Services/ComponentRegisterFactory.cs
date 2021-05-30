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
using Wings.Admin.Components.fieldIcon;
using System.Linq;
using Wings.Framework.Shared.Attributes;

namespace Wings.Admin.Services
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
            {typeof(TreeSelectFieldAttribute),typeof(FieldTreeSelect<object>)},
            {typeof(IconPickerFieldAttribute),typeof(FieldIcon<object>)},


            };

        private static Dictionary<Type, Type> registePropComponents = new Dictionary<Type, Type>(){
            {typeof(String),typeof(PropString<object>) },
            {typeof(Int32),typeof(PropString<object>)},
            {typeof(TreePageAttribute),typeof(PropTreeView<object>)},
            {typeof(DateRange),typeof(FieldDateRange)},
            {typeof(Boolean),typeof(FieldCheckbox)},
            {typeof(Enum),typeof(FieldEnum)},
            {typeof(PropTreeViewAttribute),typeof(PropTreeView<object>)}

            };

        public static void RegistComponent(Type type, ComponentBase com)
        {
            registedComponents.Add(type, com);
        }

        public static Type GetFieldDefaultComponent(PropertyInfo prop)
        {
            var type = prop.PropertyType;
            var formFieldAttribute = prop.GetCustomAttribute<FormFieldAttribute>();
            if (formFieldAttribute != null)
            {
                return registedFieldComponents[formFieldAttribute.GetType()];
            }


            if (type.IsEnum)
            {
                return registedFieldComponents[typeof(Enum)];
            }

            return registedFieldComponents[type];

        }

        public static Type GetPropDefaultComponent(PropertyInfo prop)
        {
            var type = prop.PropertyType;
            // 列表
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<object>).GetGenericTypeDefinition())
            {
                Console.WriteLine(type);
                var genericArgument = type.GetGenericArguments()[0];
                var propAttribute = prop.GetCustomAttribute<PropAttribute>();
                Console.WriteLine(prop.Name + propAttribute);
                return registePropComponents[propAttribute.GetType()];
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