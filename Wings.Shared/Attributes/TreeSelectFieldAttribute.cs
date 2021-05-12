using System;

namespace Wings.Shared.Attributes{

    public  class FormFieldAttribute:Attribute{
        /// <summary>
        /// 是否可以编辑
        /// </summary>
        /// <value></value>
       public bool Edit{get;set;}

    }

      public class TreeSelectFieldAttribute : FormFieldAttribute
    {
        /// <summary>
        /// 数据加载源
        /// </summary>
        /// <value></value>
        public string Url{get;set;}
      
        public TreeSelectFieldAttribute(string url)
        {
            Url=url;
            

        }
    }
}