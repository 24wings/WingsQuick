using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Framework.Shared.Attributes.ViewAttributes
{
    public class TreeViewAttribute : ViewAttribute
    {
        public override string ComponentType { get; set; }= "Wings.Framework.Ui.Ant.Components.AntTreeView";
    }
}
