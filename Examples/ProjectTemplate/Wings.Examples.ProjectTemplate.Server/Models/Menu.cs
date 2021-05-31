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
        public string Path { get; set; }
        public virtual Menu Parent { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
        [JsonInclude]
        public virtual List<Menu> Children { get; set; } = new List<Menu>();
        public virtual List<Role> Roles{get;set;}
    }
}