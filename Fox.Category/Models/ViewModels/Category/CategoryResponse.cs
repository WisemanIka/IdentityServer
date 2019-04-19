namespace Fox.Category.Models.ViewModels.Category
{
    public class CategoryResponse
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int OrderIndex { get; set; }
        public string Url { get; set; }
    }
}
