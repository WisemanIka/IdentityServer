using System.Collections.Generic;

namespace Fox.BasketApi.Models.ViewModels.Basket
{
    public class BasketResponse
    {
        public string UserId { get; set; }
        public List<Product> Items { get; set; }
    }
}
