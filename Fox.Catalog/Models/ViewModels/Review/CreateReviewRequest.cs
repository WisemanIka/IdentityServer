namespace Fox.Catalog.Models.ViewModels.Review
{
    public class CreateReviewRequest
    {
        public string CatalogId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
    }
}
