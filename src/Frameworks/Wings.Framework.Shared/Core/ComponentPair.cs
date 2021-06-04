using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Wings.Framework.Shared.Attributes;

namespace Wings.Framework.Shared
{
    public class ComponentPair
    {
        /// <summary>
        /// 组件
        /// </summary>
        /// <value></value>
        public Type ComponentType { get; set; }
        /// <summary>
        /// 组件显示名
        /// </summary>
        /// <value></value>
        public string ComponentDisplayName { get; set; }
        public string ComponentFullName { get; set; }
        public string DataType { get; set; }
        public bool Active { get; set; }

        public ComponentPair(Type type)
        {
            ComponentType = type;
            ComponentDisplayName = type.GetCustomAttribute<DisplayAttribute>() == null ? type.FullName : type.GetCustomAttribute<DisplayAttribute>().Name;
            DataType = type.GetCustomAttribute<ComponentDataTypeAttribute>() == null ? string.Empty : type.GetCustomAttribute<ComponentDataTypeAttribute>().DataType;
            ComponentFullName = type.FullName;
        }
    }
}