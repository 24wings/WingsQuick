using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wings.Framework.Shared.Attributes;
using Wings.Framework.Shared.Dtos;

namespace Wings.Examples.UseCase.Shared.Dvo
{
    [DataSource("/api/permission")]
    public class PermissionListDvo:BasicTree<PermissionListDvo>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string Value { get; set; }
        [Ignore]
        public List<PermissionListDvo> Children { get; set; }

    }
}
