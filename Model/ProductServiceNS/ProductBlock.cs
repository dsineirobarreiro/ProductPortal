using System.Collections.Generic;
using PracticaMad.Model.ProductoDTO;

namespace PracticaMad.Model.ProductServiceNS
{
    public class ProductBlock
    {
        public List<ProductoDTO.ProductoDTO> Products { get; private set; }
        public bool ExistMoreProducts { get; private set; }

        public ProductBlock(List<ProductoDTO.ProductoDTO> products, bool existMoreProducts)
        {
            this.Products = products;
            this.ExistMoreProducts = existMoreProducts;
        }
    }
}
