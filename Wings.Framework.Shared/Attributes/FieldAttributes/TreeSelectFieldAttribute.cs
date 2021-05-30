using System;

namespace Wings.Framework.Shared.Attributes
{
    public class TreeSelectFieldAttribute : FormFieldAttribute
    {
        /// <summary>
        /// 数据加载源
        /// </summary>
        /// <value></value>
        public string Url { get; set; }

        public TreeSelectFieldAttribute(string url)
        {
            Url = url;
        }
    }
}