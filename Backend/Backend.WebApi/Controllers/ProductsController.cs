using Backend.WebApi.Helpers;
using Backend.WebApi.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            /*
            new Product { RowKey = "1", PartitionKey="products", Name = "Æble", Category="Frugt", Price = 4d },
            new Product { RowKey = "2", PartitionKey="products", Name = "Pære", Category="Frugt", Price = 4.5d },
            new Product { RowKey = "3", PartitionKey="products", Name = "Banan", Category="Frugt", Price = 6d },
            new Product { RowKey = "4", PartitionKey="products", Name = "Ford", Category="Biler", Price = 190000d }
            */
        };
    
        private Review[] reviews =
        {
            new Review { Id = 1, ProductId = 3, Rating = 10, Text = "Banana is my favorite" },
            new Review { Id = 4, ProductId = 4, Rating = 5, Text = "Hvad sagde Han Solo hos bilforhandleren? \"Har I sår'n Ford?\"" }
        };

        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            CloudTableClient client = CreateTableClient();
            CloudTable table = client.GetTableReference("Products");

            var result = from product in table.CreateQuery<Product>() select product;

            return result;
        }

        [Route("{id}")]
        public Product GetProduct(string id)
        {
            CloudTableClient client = CreateTableClient();
            CloudTable table = client.GetTableReference("Products");

            Product result =    (
                                    from product in table.CreateQuery<Product>()
                                    where product.RowKey == id && product.PartitionKey == "products"
                                    select product
                                ).SingleOrDefault();

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

        [Route("init")]
        internal void InitializeSampleData()
        {
            CloudTableClient tableClient = CreateTableClient();
            CloudTable productsTable = tableClient.GetTableReference("Products");

            productsTable.CreateIfNotExists();

            foreach(Product p in products)
            {
                TableOperation insertProduct = TableOperation.InsertOrReplace(p);
                productsTable.Execute(insertProduct);
            }
        }

        private CloudTableClient CreateTableClient()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            return account.CreateCloudTableClient();
        }
    }
}
