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

namespace Wings.Admin
{
    public static class ComponentRegisterFactory
    {
        private static Dictionary<Type, ComponentBase> registedComponents = new Dictionary<Type, ComponentBase>();
        private static Dictionary<Type, Type> registedFieldComponents = new Dictionary<Type, Type>(){
            {typeof(String),typeof(FieldString) },
            {typeof(Int32),typeof(FieldNumber)},
            {typeof(DateTime),typeof(FieldDate)},
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



    }

}