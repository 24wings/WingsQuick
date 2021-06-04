using System.Collections.Generic;

namespace Wings.Framework.Shared.Dtos
{
    public class Paged<T>
    {
        public int Count { get; set; }
        public List<T> Data { get; set; }
    }

}