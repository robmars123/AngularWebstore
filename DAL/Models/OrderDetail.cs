using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class OrderDetail
    {
        [Key]
        public int Order_Id { get; set; }
        public int Product_Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}