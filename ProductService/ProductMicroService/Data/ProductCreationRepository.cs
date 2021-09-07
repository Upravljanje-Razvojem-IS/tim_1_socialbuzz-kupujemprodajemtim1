using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Data
{
    public class ProductCreationRepository : IProductCreationRepository
    {
        public ProductCreationConfirmation CreateProduct(ProductCreationModel productCreation)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public ProductCreationModel GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public List<ProductCreationModel> GetProducts(string productName = null, string productPrice = null, int productQuantity = 0)
        {
            throw new NotImplementedException();
        }

        public ProductCreationConfirmation UpdateProduct(ProductCreationModel productCreation)
        {
            throw new NotImplementedException();
        }
    }
}
