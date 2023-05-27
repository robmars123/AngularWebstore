using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            QuantityPerUnit = 1;
        }
        public int Product_Id { get; set; }
        public string? Product_Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? QuantityPerUnit { get; set; }
        public int Category_id { get; set; }
        public int Subcategory_id { get; set; }
        //public virtual IEnumerable<ProductImage> ProductImages { get; set; }
        [ForeignKey("Category_id")]
        public virtual Category Category { get; set; }
        [ForeignKey("Subcategory_id")]
        public virtual Subcategory Subcategory { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}
