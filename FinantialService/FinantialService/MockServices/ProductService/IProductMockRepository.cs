using FinantialService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.ProductService
{
    public interface IProductMockRepository
    {
        Product GetProductById(Guid productId);

        bool HasEnoughProducts(Guid productId, int? quantity);

        bool ProductPurchased(TransactionReduceStockDto purchased);

        bool ProductReturned(TransactionReduceStockDto returned);


    }
}
