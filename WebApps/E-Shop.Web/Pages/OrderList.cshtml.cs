using E_Shop.Web.Models.Ordering;
using E_Shop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Shop.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService)
        : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = new List<OrderModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            // assumption customerId is passed in from the UI authenticated user swn
            var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");

            var response = await orderingService.GetOrdersByCustomer(customerId);

            if (response != null && response.OrderDTOs != null)
            {
                Orders = response.OrderDTOs;
            }

            return Page();
        }
    }
}
