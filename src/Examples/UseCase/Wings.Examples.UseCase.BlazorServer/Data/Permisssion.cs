using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.BlazorServer.Data
{
    public class Permission : TreeEntityBase
    {

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string Value { get; set; }
        public virtual List<ApplicationRole> Roles { get; set; }

    }
}
