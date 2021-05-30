using System;

namespace Wings.Framework.Shared.Attributes
{
    public class ComponentDataTypeAttribute : Attribute
    {
        public string DataType { get; set; }
        public ComponentDataTypeAttribute(string dataType)
        {
            DataType = dataType;
        }

    }

}