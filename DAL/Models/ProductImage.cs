using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        public int Image_Id { get; set; }
        public byte[] Image { get; set; }
        public int Product_Id { get; set; }
        [NotMapped]
        public string ConvertedProductImage 
        {
            get {
                string imreBase64Data = Convert.ToBase64String(Image);
                return string.Format("data:image/png;base64,{0}", imreBase64Data);
            }
            set { }
        }
    }
}