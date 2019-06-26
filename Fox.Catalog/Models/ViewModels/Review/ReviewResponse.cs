using System.Collections.Generic;
using System.Linq;

namespace Fox.Catalog.Models.ViewModels.Review
{
    public class ReviewResponse
    {
        public int Rating => Reviews.Sum(x => x.Rate) / Reviews.Count;
        public List<Review> Reviews { get; set; }
    }

    public class Review
    {
        public string CatalogId { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
