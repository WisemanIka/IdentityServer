using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fox.Category.Models.ViewModels.Menu
{
    public class MenuResponse
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public string Picture { get; set; }
        public int OrderIndex { get; set; }
        public string Url { get; set; } 
        public List<MenuResponse> Childrens { get; set; }
    }
}
