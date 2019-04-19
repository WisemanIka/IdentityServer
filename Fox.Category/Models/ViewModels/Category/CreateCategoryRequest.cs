namespace Fox.Category.Models.ViewModels.Category
{
    public class CreateCategoryRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public int OrderIndex { get; set; }
        public string Url { get; set; }
        //public CategoryType Type { get; set; }
    }
}
