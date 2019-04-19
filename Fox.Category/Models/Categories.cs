using Fox.Common.Models;

namespace Fox.Category.Models
{
    public class Categories : BaseMongoCollection
    {
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
