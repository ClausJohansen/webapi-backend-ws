using Backend.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend.WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private Product[] products =
        {
            new Product { Id = 1, Name = "Æble", Category="Frugt", Price = 4M },
            new Product { Id = 2, Name = "Pære", Category="Frugt", Price = 4.5M },
            new Product { Id = 3, Name = "Banan", Category="Frugt", Price = 6M }
        };
    
        private Review[] reviews =
        {
            new Review { Id = 1, ProductId = 3, Rating = 10, Text = "Banana is my favorite" }
        };

        [Route("products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products.ToList();
        }

        public IHttpActionResult GetProduct(int id)
        {
            Product result = products.FirstOrDefault(x => x.Id == id);

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        public IEnumerable<Review> GetReviewsForProduct(int productId)
        {
            return reviews.Where(x => x.ProductId == productId);
        }
    }
}
