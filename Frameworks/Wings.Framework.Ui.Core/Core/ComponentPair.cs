using System;

namespace Wings.Framework.Ui.Core
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
        public string DataType { get; set; }
    }
}