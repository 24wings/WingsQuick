using System;

namespace Wings.Framework.Shared.Attributes
{
    public class DataSourceAttribute : Attribute
    {
        private string Url { get; set; }
        public string LoadUrl { get; }
        public string InsertUrl { get; set; }
        public string AddChild { get; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public DataSourceAttribute(string url)
        {
            Url = url;
            LoadUrl = Url + "/Load";
            AddChild = Url + "/AddChild";
            InsertUrl = Url + "/Insert";
            Update = Url + "/Update";
            Delete = Url + "/Delete";

        }
    }

}