using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Components
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly IShoppingCart _shopppingCart;
        public ShoppingCartSummary(IShoppingCart shopppingCart)
        {
            _shopppingCart = shopppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shopppingCart.GetShoppingCartItems();
            _shopppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shopppingCart, _shopppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }
    }
}
