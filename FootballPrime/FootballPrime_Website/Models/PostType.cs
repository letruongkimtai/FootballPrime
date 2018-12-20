using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    [Table("PostTypes")]
    public class PostType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Auto ID
        public int PostTypeID { get; set; }

        public string PostTypeName { get; set; }
    }
}