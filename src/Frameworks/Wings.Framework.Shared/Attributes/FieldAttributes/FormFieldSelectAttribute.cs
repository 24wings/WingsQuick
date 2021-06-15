using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wings.Framework.Shared.Attributes.FieldAttributes
{
   public class FormFieldSelectAttribute:FormFieldAttribute
    {
        public override string ComponentType { get; set; } = "Wings.Framework.Ui.Ant.Components.AntFieldSelect";
    }
}
