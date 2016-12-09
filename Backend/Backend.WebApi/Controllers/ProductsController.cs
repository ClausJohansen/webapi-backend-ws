using Backend.WebApi.Helpers;
using Backend.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private Product[] products =
        {
            new Product { Id = 1, Name = "Æble", Category="Frugt", Price = 4M },
            new Product { Id = 2, Name = "Pære", Category="Frugt", Price = 4.5M },
            new Product { Id = 3, Name = "Banan", Category="Frugt", Price = 6M },
            new Product { Id = 4, Name = "Ford", Category="Biler", Price = 190000M },
        };
    
        private Review[] reviews =
        {
            new Review { Id = 1, ProductId = 3, Rating = 10, Text = "Banana is my favorite" },
            new Review { Id = 4, ProductId = 4, Rating = 5, Text = "Hvad sagde Han Solo hos bilforhandleren? \"Har I sår'n Ford?\"" }
        };

        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products.ToList();
        }

        [Route("{id}")]
        public Product GetProduct(int id)
        {
            Product result = products.FirstOrDefault(x => x.Id == id);

            if (result == null)
                throw new NotFoundException();
            else
                return result;
        }

        [Route("{productId}/reviews")]
        public IEnumerable<Review> GetReviewsForProduct(int productId)
        {
            return reviews.Where(x => x.ProductId == productId);
        }
    }
}
