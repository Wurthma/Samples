using ProductsApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProductsApp.Controllers
{
    //Exemplo API
    //https://docs.microsoft.com/pt-br/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
            new Product { Id = 4, Name = "Box", Category = "Hardware", Price = 12.50M },
            new Product { Id = 5, Name = "Table", Category = "Furniture", Price = 82.99M },
            new Product { Id = 6, Name = "Chair", Category = "Furniture", Price = 23.10M },
            new Product { Id = 7, Name = "Screwdriver", Category = "Hardware", Price = 5.60M },
            new Product { Id = 8, Name = "Doll", Category = "Toys", Price = 29.50M },
            new Product { Id = 9, Name = "Couch", Category = "Furniture", Price = 210.00M },
            new Product { Id = 10, Name = "Potato", Category = "Groceries", Price = 0.70M }
        };

        //URL:	/api/products
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        //URL: /api/products/id
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
