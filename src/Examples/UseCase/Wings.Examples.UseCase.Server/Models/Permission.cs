using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wings.Examples.UseCase.Server.Services.Repositorys;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Permission:TreeEntity
    {
        
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string Value { get; set; }
        public virtual List<RbacRole> Roles { get; set; }

    }
}
