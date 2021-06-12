using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Wings.Examples.UseCase.Server.Services.Repositorys;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Menu:TreeEntity,BaseEntity
    {
        public override int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 菜单的树路径
        /// </summary>
        /// <value></value>
        public override string TreePath { get; set; }

        public override int? ParentId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;

        public virtual List<RbacRole> Roles { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}