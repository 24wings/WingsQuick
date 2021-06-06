using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Role:IdentityRole<int>
    {
       

        public string Code { get; set; }

        //public virtual List<Menu> Menus { get; set; } = new List<Menu>();
    }
}