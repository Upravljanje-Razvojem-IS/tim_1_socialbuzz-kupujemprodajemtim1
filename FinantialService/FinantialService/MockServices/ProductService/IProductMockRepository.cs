using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.ProductService
{
    public interface IProductMockRepository
    { 
        bool ProductExistsById(Guid productId);

        bool HasEnoughProducts(Guid productId, int? quantity);
    }
}
