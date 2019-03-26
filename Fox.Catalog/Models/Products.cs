using System.Collections.Generic;
using Fox.Common.Models;

namespace Fox.Catalog.Models
{
    public class Products : BaseMongoCollection
    {
        public List<string> CategoryId { get; set; }
        public string ProviderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Main Image
        public string PrimaryImage { get; set; }
        //All Images
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
