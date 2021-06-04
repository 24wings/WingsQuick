using System;
using System.Collections.Generic;

namespace Wings.Shared.Dto
{



    public class BasicTree
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual List<BasicTree> Children { get; set; }
        public virtual string Icon { get; set; }
        public virtual int? ParentId { get; set; }
        public virtual object OriginData { get; set; }
    }

    public interface IBasicTree
    {
        int Id { get; set; }
        string Title { get; set; }
        List<BasicTree> Children { get; set; }
        string Icon { get; set; }
        int? ParentId { get; set; }
        object OriginData { get; set; }
    }

}