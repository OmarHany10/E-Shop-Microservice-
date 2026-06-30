using JasperFx;

namespace Basket.API.Models
{
    public class ShoppinCart
    {
        public ShoppinCart()
        {
            
        }

        public ShoppinCart(string username)
        {
            UserName = username;
        }

        [Identity]
        public string UserName { get; set; } = default;
        public List<ShoppinCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);


    }
}
