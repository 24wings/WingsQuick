using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Shared.Dvo
{
    [CRUDModel(
        Create = typeof(MenuCreateDvo),
        Update = typeof(MenuCreateDvo)
    )]
    [DataSource("/api/Menu")]
    [TreePage("菜单管理")]
    public class MenuListDvo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public List<MenuListDvo> Children { get; set; }
        public string Icon { get; set; }
    }
    [DataSource("/api/Menu")]
    [Display(Name = "新建菜单")]
    public class MenuCreateDvo
    {
        public int Id { get; set; }
        [Display(Name = "菜单名")]
        [Required]
        public string Text { get; set; }
        [Display(Name = "菜单编码")]
        [Required]
        public string Code { get; set; }
        public int ParentId { get; set; }
        public string Icon { get; set; }
    }



}