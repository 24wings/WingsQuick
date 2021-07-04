using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Wings.Framework.Ui.Core.Components;
using System.ComponentModel.DataAnnotations;
using System;
using Wings.Framework.Shared.Attributes;
using System.Diagnostics.CodeAnalysis;
using Wings.Framework.Shared;

namespace Wings.Framework.Ui.Core.Services
{
    public static class DynamicComponentScanner
    {
        public static List<IThemeConfig> ThemeList = new List<IThemeConfig>();
        public static IThemeConfig CurrentTheme = null;
        public static List<ComponentPair> ComponentPairs { get; set; } = new List<ComponentPair>();
        private static List<ComponentPair> PropComponentPairs { get; set; } = new List<ComponentPair>();
        private static List<ComponentPair> FieldComponentPairs { get; set; } = new List<ComponentPair>();
        private static List<ComponentPair> ViewComponentPairs { get; set; } = new List<ComponentPair>();
        /// <summary>
        /// 根据Assembly扫描动态组件
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static List<ComponentPair> ScanDynmaicComponentByAssembly(Assembly assembly)
        {
            return assembly.DefinedTypes.Where(t => t.IsSubclassOf(typeof(DynamicComponentBase)) && !t.IsAbstract).Select(com => new ComponentPair(com.AsType())).ToList();
        }
        /// <summary>
        /// 添加注册组件
        /// </summary>
        /// <param name="componentPairs"></param>
        public static void AddComponentPairs(List<ComponentPair> componentPairs)
        {
            ComponentPairs.AddRange(componentPairs);
            PropComponentPairs.AddRange(componentPairs.Where(pair => pair.ComponentType.HasImplementedRawGeneric(typeof(PropertyComponentBase<>)) && !pair.ComponentType.IsAbstract).ToList());
            FieldComponentPairs.AddRange(componentPairs.Where(pair => pair.ComponentType.HasImplementedRawGeneric(typeof(FieldComponentBase<>)) && !pair.ComponentType.IsAbstract).ToList());
            ViewComponentPairs.AddRange(componentPairs.Where(pair => pair.ComponentType.HasImplementedRawGeneric(typeof(ModelComponentBase<>)) && !pair.ComponentType.IsAbstract).ToList());

        }

        public static void ClearComponentPairs()
        {
            ComponentPairs = new List<ComponentPair>();
        }

        public static Type GetViewComponentType(Type type)
        {
            Console.WriteLine(type);
            Console.WriteLine(type.GetCustomAttribute<ViewAttribute>(true));
            var name = type.GetCustomAttribute<ViewAttribute>(true).ComponentType;
            return ViewComponentPairs.Where(type => type.ComponentFullName.Contains(name)).FirstOrDefault().ComponentType;

        }
        public static Type GetPropComponentTypeByProperty<TModel>(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            Type componentType = null;
            if (propertyType.IsGenericType)
            {
                var genericTypeDefinition = propertyType.GetGenericTypeDefinition();
                // nullable类型
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    var type = propertyType.GetGenericArguments()[0];

                    componentType = PropComponentPairs.Where(pair => pair.DataType == type.FullName).FirstOrDefault().ComponentType;
                }
                // list 类型
                if (genericTypeDefinition == typeof(List<object>).GetGenericTypeDefinition())
                {

                    var propAttribute = property.GetCustomAttribute<PropAttribute>();
                    Console.WriteLine("list:" + propAttribute.ComponentType);
                    try
                    {
                        componentType = PropComponentPairs.Where(pair => pair.ComponentType.FullName.Contains(propAttribute.ComponentType)).FirstOrDefault()?.ComponentType;
                        Console.WriteLine("get type:" + componentType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("====" + e);
                    }

                }
                if (propertyType.IsEnum)
                {
                    componentType = PropComponentPairs.Where(pair => pair.DataType == typeof(Enum).FullName).FirstOrDefault().ComponentType;
                }
            }
            else
            {
                // Console.WriteLine("system basic data type:" + propertyType.FullName);

                var pair = PropComponentPairs.Where(pair => pair.DataType == propertyType.FullName).FirstOrDefault();
                if (pair != null)
                {
                    componentType = pair.ComponentType;
                }
            }
            if (componentType == null)
            {
                Console.WriteLine("error: not found component for property type:" + propertyType + "   prop component count:" + PropComponentPairs.Count);
                return null;
            }
            if (componentType.IsGenericType)
            {
                componentType = componentType.MakeGenericType(typeof(TModel));
            }
            return componentType;
        }


        public static Type GetFieldComponentTypeByProperty<TModel>(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            Type componentType = null;
            var fieldAttribute = property.GetCustomAttribute<FormFieldAttribute>();

            if (propertyType.IsGenericType)
            {
                var genericTypeDefinition = propertyType.GetGenericTypeDefinition();
                // nullable类型
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    var type = propertyType.GetGenericArguments()[0];
                    Console.WriteLine("is nullable" + type.FullName);

                    componentType = FieldComponentPairs.Where(pair => pair.DataType == type.FullName).FirstOrDefault().ComponentType;
                }
                // list 类型
                if (genericTypeDefinition == typeof(List<object>).GetGenericTypeDefinition())
                {

               
                    try
                    {
                        componentType = PropComponentPairs.Where(pair => pair.ComponentType.FullName.Contains(fieldAttribute.ComponentType)).FirstOrDefault()?.ComponentType;
                        Console.WriteLine("get type:" + componentType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("====" + e);
                    }
                }
                if (propertyType.IsEnum)
                {
                    componentType = FieldComponentPairs.Where(pair => pair.DataType == typeof(Enum).FullName).FirstOrDefault().ComponentType;
                }
            }
            else
            {
                Console.WriteLine("pair:");
                componentType = FieldComponentPairs.Where(pair => pair.DataType == propertyType.FullName).FirstOrDefault()?.ComponentType;
            }

            if ( fieldAttribute!=null)
            {
                try
                {
                    Console.WriteLine("attribute type:" + fieldAttribute.ComponentType);

                    componentType = FieldComponentPairs.Where(pair => pair.ComponentType.FullName.Contains(fieldAttribute.ComponentType)).FirstOrDefault()?.ComponentType;
                    if (componentType != null)
                    {
                        Console.WriteLine("yes found componentType:" + componentType.FullName);
                    }
                }catch(Exception e)
                {
                    Console.WriteLine("exception: not found" + e);
                    Console.WriteLine(e);
                }
                

            }
            if(componentType==null)
            {
                Console.WriteLine("error: not found component for field type:" + propertyType);
                return null;
            }
            if (componentType.IsGenericType)
            {
                Console.WriteLine(typeof(TModel).Name);
                componentType = componentType.MakeGenericType(typeof(TModel));
            }
            return componentType;
        }

        /// <summary>
        /// 判断指定的类型 <paramref name="type"/> 是否是指定泛型类型的子类型，或实现了指定泛型接口。
        /// </summary>
        /// <param name="type">需要测试的类型。</param>
        /// <param name="generic">泛型接口类型，传入 typeof(IXxx&lt;&gt;)</param>
        /// <returns>如果是泛型接口的子类型，则返回 true，否则返回 false。</returns>
        public static bool HasImplementedRawGeneric([NotNull] this Type type, [NotNull] Type generic)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (generic == null) throw new ArgumentNullException(nameof(generic));

            // 测试接口。
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 测试类型。
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试某个类型是否是指定的原始接口。
            bool IsTheRawGenericType(Type test)
                => generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }


    }

}