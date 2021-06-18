using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wings.Examples.UseCase.Server.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public string Avatar { get; set; }

        public string ItemPicUrl { get; set; }

        public string SubTitle { get; set; }

        public virtual TopicCategory TopicCategory { get; set; }
        
        public decimal PriceInfo { get; set; }


        public string ReadCount { get; set; }

        public string ScenePicUrl { get; set; }
        public int TemplateId { get; set; }

        public int TopicTagId { get; set; }

        public int SortOrder { get; set; }

        public bool IsShow { get; set; }
    }


    public class TopicCategory
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }


        public string PicUrl { get; set; }
    }
}
