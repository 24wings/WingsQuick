using System;

namespace Wings.Framework.Shared.Attributes
{
    public class DataSourceAttribute : Attribute
    {
        private string Url { get; set; }
        public string LoadUrl { get; set; }
        public string InsertUrl { get; set; }
        public string AddChild { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public string Detail { get; set; }
        public DataSourceAttribute(string url)
        {
            Url = url;
            LoadUrl = Url + "/Load";
            AddChild = Url + "/AddChild";
            InsertUrl = Url + "/Insert";
            Update = Url + "/Update";
            Delete = Url + "/Delete";
            Detail = Url + "/Detail";

        }
        public int PageSize { get; set; } = 10;
    }

}