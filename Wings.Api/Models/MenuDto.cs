using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace Wings.Api.Models
{
    [AutoMap(typeof(Menu))]

    public class MenuDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        [SourceMember("Name")]
        public string Name2 { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;

        public List<Menu> Children { get; set; } = new List<Menu>();
    }
}