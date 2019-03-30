namespace Fox.Catalog.Models.ViewModels.Product
{
    public class GetProductRequest
    {
        public string Id { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }


        public string SearchText { get; set; }
    }
}
