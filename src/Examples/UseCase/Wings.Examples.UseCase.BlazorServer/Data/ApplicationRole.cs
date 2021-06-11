using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.BlazorServer.Data
{
 
    public class ApplicationRole : IdentityRole<int>
    {
        public string Code { get; set; }

        public virtual List<Menu> Menus { get; set; } = new List<Menu>();

        public virtual List<Permission> Permissions { get; set; }
    }
}
