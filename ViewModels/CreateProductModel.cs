using DAL.Models;

namespace ReactWebstore.ViewModels
{
    public class CreateProductModel
    {
        public Product Product { get; set; }
        public List<Product> productList { get; set; }
        public ProductImage PrimaryProduct_Image { get; set; }
        public List<ProductImage> productImagesList { get; set; }

        public virtual ProductImage productImage { get; set; }

        public virtual Category Categories { get; set; }

        public virtual Subcategory Subcategories { get; set; }
    }
}
