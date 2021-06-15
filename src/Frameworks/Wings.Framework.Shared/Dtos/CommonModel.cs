using System;
using System.Collections.Generic;

namespace Wings.Framework.Shared.Dtos
{
    public interface ILabel
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
    public interface BasicTree<T> 
    {
         int Id { get; set; }
         int? ParentId { get; set; }

        List<T> Children { get; set; } 
    }
    public interface IRange<T>

    {

        T Start { get; }

        T End { get; }

        bool Includes(T value);

        bool Includes(IRange<T> range);

    }



    public class DateRange : IRange<Nullable<DateTime>>

    {

        public DateRange(DateTime? start, DateTime? end)

        {

            Start = start;

            End = end;

        }



        public DateTime? Start { get; private set; }

        public DateTime? End { get; private set; }



        public bool Includes(DateTime? value)

        {

            return (Start <= value) && (value <= End);

        }



        public bool Includes(IRange<DateTime?> range)

        {

            return (Start <= range.Start) && (range.End <= End);

        }


    }
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