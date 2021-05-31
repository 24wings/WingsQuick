using System;

namespace Wings.Framework.Shared.Attributes
{
    public class PropAttribute : Attribute
    {
        public virtual string ComponentType { get; set; }

    }

    public class PropTreeViewAttribute : PropAttribute
    {
        public override string ComponentType { get; set; } = "Wings.Framework.Ui.Ant.Components.AntPropTreeView";
    }
}
