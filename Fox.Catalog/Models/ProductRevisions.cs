using System;
using System.Collections.Generic;
namespace Fox.Catalog.Models
{
    public class ProductRevisions
    {
        public string Id { get; set; }
        public List<Products> Revisions { get; set; }
    }
}
