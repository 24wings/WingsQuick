using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public enum MediaType
    {
        Image = 1
    }
    public class Ad
    {
        public int Id { get; set; }
        public  int AdPositionId { get; set; }
        public virtual AdPosition AdPosition { get; set; }
        public MediaType MediaType { get; set; }
        public string Name { get; set; }

        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }

        public DateTime EndTime { get; set; }

        public bool Enable { get; set; }
    }

    public class AdPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Desc { get; set; }

    }
}
