using System.Collections.Generic;

namespace Fox.Basket.Models.ViewModels.Basket
{
    public class CreateBasketRequest
    {
        public string UserId { get; set; }
        public List<Product> Items { get; set; }
    }
}
