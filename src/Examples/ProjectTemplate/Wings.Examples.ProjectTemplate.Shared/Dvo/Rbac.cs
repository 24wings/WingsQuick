using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Framework.Shared.Attributes;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Shared.Dvo
{
    [CrudModel(
        Create = typeof(MenuCreateDvo),
        Update = typeof(MenuCreateDvo)
    )]
    [DataSource("/api/Menu")]
    [TreePage("菜单管理")]
    public class MenuListDvo
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "菜单名")]
        public string Title { get; set; }
        [Display(Name = "菜单代码")]
        public string Code { get; set; }
        [Display(Name = "上级Id")]
        public int? ParentId { get; set; }
        [Display(Name = "链接")]
        public string Path { get; set; }
        [DetailView(Show = false)]
        [Editable(false)]
        [PropTreeView]
        [Display(Name = "下级菜单")]
        public List<MenuListDvo> Children { get; set; }
        [Display(Name = "图标")]
        public string Icon { get; set; }
    }
    [DataSource("/api/Menu")]
    [Display(Name = "新建菜单")]
    public class MenuCreateDvo
    {
        public int Id { get; set; }
        [Display(Name = "菜单名")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "菜单编码")]
        [Required]
        public string Code { get; set; }
        [Display(Name = "上级菜单")]
        [Editable(false)]
        public int ParentId { get; set; }
        [IconPickerField]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        [Display(Name = "链接")]
        public string Path { get; set; }
    }



}