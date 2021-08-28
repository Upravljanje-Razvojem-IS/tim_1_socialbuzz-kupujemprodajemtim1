using FinantialService.Models;
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
                    Quantity = 5,
                    Price = 12000
                },
                new Product
                {
                    ProductId = Guid.Parse("b6496c4a-f938-4863-b3b7-2db52e4271dc"),
                    Name = "Rukavice za bicikl Capriolo",
                    Quantity = 15,
                     Price = 2500
                },
                new Product
                {
                    ProductId = Guid.Parse("49420693-be98-4552-bb29-1033a27a44e1"),
                    Name = "Scott bicikl 17'",
                    Quantity = 5,
                     Price = 56700
                },
                new Product
                {
                    ProductId = Guid.Parse("affaf92b-3ce6-4b30-bed8-b144829d2089"),
                    Name = "Gumice za kosu",
                    Quantity = 54,
                     Price = 120
                },
                new Product
                {
                    ProductId = Guid.Parse("b6620706-88bb-4a02-b748-241c07de01c7"),
                    Name = "Novcanik Enzo & Lorenzo",
                    Quantity = 13,
                    Price = 1500
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

        public Product GetProductById(Guid productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public bool ProductPurchased(TransactionReduceStockDto purchased)
        {
            Product product = Products.FirstOrDefault(p => p.ProductId == purchased.ProductId);
            if (product != null)
            {
                product.Quantity -= purchased.Quantity;
                return true;
            }
            return false;
        }

        public bool ProductReturned(TransactionReduceStockDto returned)
        {
            Product product = Products.FirstOrDefault(p => p.ProductId == returned.ProductId);
            if (product != null)
            {
                product.Quantity += returned.Quantity;
                return true;
            }
            return false;
        }
    }
}
