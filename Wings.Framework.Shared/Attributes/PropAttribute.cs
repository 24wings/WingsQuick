using System;

namespace Wings.Framework.Shared.Attributes
{
    public class PropAttribute : Attribute
    {
        public string ComponentFullName { get; set; } = "Wings.Framework.Ui.Ant.Components.AntPropString";

    }

    public class PropTreeViewAttribute : PropAttribute
    {

    }
}
