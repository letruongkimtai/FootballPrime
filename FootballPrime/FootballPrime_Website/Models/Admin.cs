using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballPrime_Website.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int AdmID { get; set; }
        public int AdmName { get; set; }
        public int AdmPwd { get; set; }
    }
}