using System;

namespace Wings.Framework.Shared.Attributes
{

    public class SearchBarAttribute : System.Attribute
    {
        public Type SearchBarComponentType { get; set; }
        public SearchBarAttribute(Type type)
        {
            SearchBarComponentType = type;
        }

    }

}