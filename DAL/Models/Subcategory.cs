using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    [Table("Subcategories")]
    public class Subcategory
    {
        [Key]
        public int Subcategory_Id { get; set; }
        public string Subcategory_Name { get; set; }
    }
}