using System.Collections.Generic;

namespace Fox.BasketApi.Models.ViewModels.Basket
{
    public class CreateBasketRequest
    {
        public string UserId { get; set; }
        public List<Product> Items { get; set; }
    }
}
