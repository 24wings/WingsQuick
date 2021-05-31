using System;
namespace Wings.Shared.Attributes
{
  

    public class PageAttribute : Attribute
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        /// <value></value>
        public string Title { get; set; }
        /// <summary>
        /// 页面子标题
        /// </summary>
        /// <value></value>
        public string SubTitle { get; set; }

        public PageAttribute(string title, string subTitle = "")
        {
            Title = title;
            SubTitle = subTitle;


        }



    }

    public class TreePageAttribute : PageAttribute
    {
        public string IdKey { get; set; }
        public string ParentIdKey { get; set; }
        public TreePageAttribute(string title, string subTitle = "") : base(title, subTitle)
        {


        }


    }
    /// <summary>
    /// 表格页面
    /// </summary>
    public class TablePageAttribute : PageAttribute
    {
        public TablePageAttribute(string title, string subTitle = "") : base(title, subTitle)
        {



        }
    }


}