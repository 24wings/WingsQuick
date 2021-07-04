using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Framework.Shared.Attributes.ViewAttributes;

namespace Wings.Framework.Shared.Attributes
{
    public class DataGridAttribute:ViewAttribute
    {
        public override string ComponentType { get; set; } = "Wings.Framework.Ui.Ant.Components.AntTableView";
    }

}
