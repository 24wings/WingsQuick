using System;

namespace Wings.Framework.Shared.Attributes
{
    public class DataSourceAttribute : Attribute
    {
        private string Url { get; set; }
        public string Load { get; }
        public string Insert { get; set; }
        public string AddChild { get; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public DataSourceAttribute(string url)
        {
            Url = url;
            Load = Url + "/Load";
            AddChild = Url + "/AddChild";
            Insert = Url + "/Insert";
            Update = Url + "/Update";
            Delete = Url + "/Delete";

        }
    }

}