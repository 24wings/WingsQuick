using System.Collections.Generic;
using Wings.Shared.Attributes;
using Wings.Shared.Dto;

namespace Wings.Shared.Dvo
{
    [CRUDModel(Create = typeof(MenuCreateDvo))]
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

    public class MenuCreateDvo
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Code { get; set; }
        public int ParentId { get; set; }
        public string Icon { get; set; }
    }
}