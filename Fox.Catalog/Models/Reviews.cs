using Fox.Common.Models;

namespace Fox.Catalog.Models
{
    public class Reviews : BaseMongoCollection
    {
        public string CatalogId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
