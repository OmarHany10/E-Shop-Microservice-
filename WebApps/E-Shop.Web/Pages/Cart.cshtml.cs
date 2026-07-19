using E_Shop.Web.Models.Basket;
using E_Shop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace E_Shop.Web.Pages
{
    public class CartModel(IBasketService basketService)
        : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        Cart = await basketService.LoadUserBasket();

        Cart.Items.RemoveAll(x => x.ProductId == productId);

        await basketService.StoreBasket(new StoreBasketRequest(Cart));

        return RedirectToPage();
    }
    }
}
