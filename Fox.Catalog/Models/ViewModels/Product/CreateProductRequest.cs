
using System.Collections.Generic;

namespace Fox.Catalog.Models.ViewModels.Product
{
    public class CreateProductRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
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
