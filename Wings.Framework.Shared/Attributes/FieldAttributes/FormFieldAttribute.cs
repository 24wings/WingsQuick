using System;

namespace Wings.Framework.Shared.Attributes
{
    public class FormFieldAttribute : Attribute
    {
        /// <summary>
        /// 是否可以编辑
        /// </summary>
        /// <value></value>
        public bool Edit { get; set; } = true;
        public Type ComponentType { get; set; }
    }

}