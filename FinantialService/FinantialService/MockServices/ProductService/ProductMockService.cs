using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinantialService.MockServices.ProductService
{
    public class ProductMockService : IProductMockRepository
    {
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            new Product
                {
                    ProductId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Name = "Nike Air Jordan 2020",
                    Quantity = 5
                },
            new Product
                {
                    ProductId = Guid.Parse("b6496c4a-f938-4863-b3b7-2db52e4271dc"),
                    Name = "Rukavice za bicikl Capriolo",
                    Quantity = 15
                },
            new Product
                {
                    ProductId = Guid.Parse("49420693-be98-4552-bb29-1033a27a44e1"),
                    Name = "Scott bicikl 17'",
                    Quantity = 5
                },
            new Product
                {
                    ProductId = Guid.Parse("affaf92b-3ce6-4b30-bed8-b144829d2089"),
                    Name = "Gumice za kosu",
                    Quantity = 54
                },
            new Product
                {
                    ProductId = Guid.Parse("b6620706-88bb-4a02-b748-241c07de01c7"),
                    Name = "Novcanik Enzo & Lorenzo",
                    Quantity = 13
                }
        };



        public bool HasEnoughProducts(Guid productId, int? quantity)
        {
            Product product = Products.FirstOrDefault(p => p.ProductId == productId);
            if (product.Quantity < quantity)
            {
                return false;
            }
            return true;
        }

        public bool ProductExistsById(Guid productId)
        {
            if (Products.FirstOrDefault(p => p.ProductId == productId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
