using System.Collections.Generic;

namespace Wings.Framework.Shared.Dtos
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class MenuData
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public List<MenuData> Children { get; set; }
        public int? ParentId { get; set; }
        public MenuData Parent { get; set; }

    }

}