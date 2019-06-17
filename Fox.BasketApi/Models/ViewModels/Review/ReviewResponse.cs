using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fox.BasketApi.Models.ViewModels.Review
{
    public class ReviewResponse
    {
        public string CatalogId { get; set; }
        public int Rating => Reviews.Sum(x => x.Rating) / Reviews.Count;
        public List<Review> Reviews { get; set; }
    }

    public class Review
    {
        public string Fullname { get; set; }
        public string UserImg { get;set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
