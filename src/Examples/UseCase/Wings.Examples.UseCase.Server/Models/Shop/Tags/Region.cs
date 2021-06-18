using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public enum RegionType
    {
        Country=0,
        Province,
        City,
        Area
    }
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; } = 0;
        public string Name { get; set; }

        public RegionType Type { get; set; }

        public int AgencyId { get; set; }
    }
}
