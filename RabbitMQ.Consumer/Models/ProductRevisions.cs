using System;
using System.Collections.Generic;

namespace RabbitMQ.Consumer.Models
{
    public class ProductRevisions
    {
        public string Id { get; set; }
        public List<Products> Revisions { get; set; }
    }

    public class ProductResponse : Products
    {
        public string Id { get; set; }
    }

    public class Products
    {
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public List<string> CategoryId { get; set; }
        public string ProviderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryImage { get; set; }
        public List<string> Images { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string Quantity { get; set; }
        public string Video { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Colors { get; set; }
        public string Weight { get; set; }
        public bool IsActive { get; set; }
    }
}
