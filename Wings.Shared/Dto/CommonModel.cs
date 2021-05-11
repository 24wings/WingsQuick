using System.Collections.Generic;

namespace Wings.Shared.Dto
{
    /// <summary>
    /// 通用基础查询对象
    /// </summary>
    public class BasicQuery
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasicQueryResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();

        public int Total { get; set; }

    }

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