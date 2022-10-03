using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        } 

        public IActionResult Checkout()//GET => HttpGet is default
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some pies first.");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order";
            return View();
        }
    }
}
