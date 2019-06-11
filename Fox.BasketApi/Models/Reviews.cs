using Fox.Common.Models;

namespace Fox.BasketApi.Models
{
    public class Reviews : BaseMongoCollection
    {
        public string CatalogId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
