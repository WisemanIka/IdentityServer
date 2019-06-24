namespace Fox.Catalog.Models.ViewModels.Review
{
    public class GetReviewRequest
    {
        public string Id { get; set; }
        public string CatalogId { get; set; }
        public bool? IsActive { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
