using Fox.Common.Models;

namespace Fox.Catalog.Models
{
    public class Reviews : BaseMongoCollection
    {
        public string CatalogId { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        public bool IsActive { get; set; }
    }
}
