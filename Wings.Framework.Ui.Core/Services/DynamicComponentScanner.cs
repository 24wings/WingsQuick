using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Wings.Framework.Ui.Core.Components;
using System.ComponentModel.DataAnnotations;
using System;
using Wings.Framework.Shared.Attributes;

namespace Wings.Framework.Ui.Core.Services
{
    public static class DynamicComponentScanner
    {
        public static List<ComponentPair> ComponentPairs { get; set; } = new List<ComponentPair>();
        /// <summary>
        /// 根据Assembly扫描动态组件
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<ComponentPair> ScanDynmaicComponentByAssembly(Assembly assembly)
        {
            return assembly.DefinedTypes.Where(t => t.IsSubclassOf(typeof(DynamicComponentBase)) && !t.IsAbstract).Select(com => new ComponentPair
            {
                ComponentFullName = com.FullName,
                ComponentDisplayName = com.GetCustomAttribute<DisplayAttribute>() == null ? com.FullName : com.GetCustomAttribute<DisplayAttribute>().Name,
                DataType = com.GetCustomAttribute<ComponentDataTypeAttribute>() == null ? string.Empty : com.GetCustomAttribute<ComponentDataTypeAttribute>().DataType
            }).ToList();
        }
        /// <summary>
        /// 添加注册组件
        /// </summary>
        /// <param name="componentPairs"></param>
        public static void AddComponentPairs(List<ComponentPair> componentPairs)
        {
            ComponentPairs.AddRange(componentPairs);

        }

        public static void ClearComponentPairs()
        {
            ComponentPairs = new List<ComponentPair>();
        }

        public static Type GetPropComponentTypeByProperty(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            var componentFullName = string.Empty;
            if (propertyType.IsGenericType)
            {
                var genericTypeDefinition = propertyType.GetGenericTypeDefinition();
                // nullable类型
                if (genericTypeDefinition == typeof(Nullable))
                {
                    var type = genericTypeDefinition.GetGenericArguments()[0];
                    componentFullName = ComponentPairs.Where(pair => pair.DataType == type.FullName).FirstOrDefault().ComponentFullName;
                }
                // list 类型
                if (genericTypeDefinition == typeof(List<object>).GetGenericTypeDefinition())
                {

                    var propAttribute = property.GetCustomAttribute<PropAttribute>();
                    componentFullName = ComponentPairs.Where(pair => pair.DataType == propAttribute.ComponentFullName).FirstOrDefault().ComponentFullName;
                }
                if (propertyType.IsEnum)
                {
                    componentFullName = ComponentPairs.Where(pair => pair.DataType == typeof(Enum).FullName).FirstOrDefault().ComponentFullName;
                }
            }


            return Type.GetType(componentFullName + $"<{property.DeclaringType.FullName}>");
        }




    }

}