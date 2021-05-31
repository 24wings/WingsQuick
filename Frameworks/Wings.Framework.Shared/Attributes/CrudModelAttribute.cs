using System;

namespace Wings.Framework.Shared.Attributes
{
    public class CrudModelAttribute : Attribute
    {
        public Type Create { get; set; }
        public Type Update { get; set; }

    }

}