using ProductMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMicroService.Data
{
    public interface IProductCreationRepository
    {
        List<ProductCreationModel> GetProducts(string productName = null, string productPrice = null, int productQuantity = 0);
        ProductCreationModel GetProductById(Guid productId);

        ProductCreationConfirmation CreateProduct(ProductCreationModel productCreation);

        ProductCreationConfirmation UpdateProduct(ProductCreationModel productCreation);


        void DeleteProduct(Guid productId);
    }
} 
