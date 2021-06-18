using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class GoodIssue
    {
        public int Id { get; set; }

        public int GoodId { get; set; }
        public virtual Good Good { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
