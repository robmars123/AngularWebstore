using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class CartItem
    {
        [Key]
        public int CartItem_Id { get; set; }
        public string User_Id { get; set; }
        public int Cart_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        public int Payment_Status { get; set; }
    }
}