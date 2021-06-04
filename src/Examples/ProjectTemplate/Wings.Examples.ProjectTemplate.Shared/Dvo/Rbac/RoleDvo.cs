using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Framework.Shared.Attributes;
using Wings.Shared.Attributes;

namespace Wings.Shared.Dvo
{
    [DataSource("/api/Role")]
    [TablePage("角色管理")]
    [Display(Name = "角色管理")]
    public class RoleListDvo
    {
        [Key]
        [FormField(Edit = false)]
        public int Id { get; set; }


        [Display(Name = "角色名")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "角色代码")]
        [Required]

        public string Code { get; set; }

        [PropTreeView]
        [TreeSelectField("/api/Menu/Load")]
        [Display(Name = "角色菜单")]
        public List<MenuListDvo> MenuList { get; set; }

    }

    public class MenuTreeSelectDvo
    {

    }
}