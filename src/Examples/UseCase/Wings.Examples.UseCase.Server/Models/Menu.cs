using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Wings.Api.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 菜单的树路径
        /// </summary>
        /// <value></value>
        public string TreePath { get; set; }

        public int? ParentId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;

        public virtual List<Role> Roles { get; set; }
    }
}