using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.ProductService
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }

}
