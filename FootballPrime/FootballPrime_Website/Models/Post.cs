using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Author { get; set; }

        public string Img { get; set; }

        public string Content { get; set; }

        public int PostTypeID { get; set; }
        public virtual PostType PostType { get; set; }
    }
}