using System;
namespace Wings.Shared.Attributes
{
    public class DataSourceAttribute : Attribute
    {
        private String Url { get; set; }
        public String Load { get; }
        public String AddChild { get; }
        public DataSourceAttribute(string url)
        {
            Url = url;
            Load = Url + "/Load";
            AddChild = Url + "/AddChild";

        }
    }

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
}