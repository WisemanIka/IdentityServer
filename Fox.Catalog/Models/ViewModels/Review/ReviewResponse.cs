using System.Collections.Generic;
using System.Linq;

namespace Fox.Catalog.Models.ViewModels.Review
{
    public class ReviewResponse
    {
        public int Rating => Reviews.Sum(x => x.Rate) / Reviews.Count;
        public List<Reviews> Reviews { get; set; }
    }
}
