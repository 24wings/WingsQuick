using System;

namespace Wings.Shared.Attributes
{
    public class CRUDModelAttribute : Attribute
    {
        public Type Create { get; set; }

    }
}