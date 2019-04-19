namespace Fox.Category.Models.ViewModels.Category
{
    public class GetCategoryRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
        
        public string SearchText { get; set; }
    }
}
