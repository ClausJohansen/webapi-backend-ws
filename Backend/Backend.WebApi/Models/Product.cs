using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.WebApi.Models
{
    public class Product : TableEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
}