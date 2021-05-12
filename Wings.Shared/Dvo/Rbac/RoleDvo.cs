using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Shared.Attributes;

namespace Wings.Shared.Dvo
{
    [DataSource("/api/Role")]
    [TablePage("角色管理")]
    [Display(Name = "角色管理")]
    public class RoleListDvo
    {
        public int Id { get; set; }
        [Display(Name = "角色名")]
        public string Name { get; set; }
        [Display(Name = "角色代码")]
        public string Code { get; set; }
        public List<MenuListDvo> MenuList { get; set; }

    }

    public class MenuTreeSelectDvo
    {

    }
}