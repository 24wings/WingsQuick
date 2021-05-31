using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wings.Api.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual List<Menu> Menus { get; set; } = new List<Menu>();
    }
}