using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.Repository;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repository = repoService;
            _cart = cartService;
        }

        [Authorize]
        public ViewResult List() =>
            View(_repository.Orders.Where(o => !o.IsShipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderId)
        {
            Order order = _repository.Orders
                .FirstOrDefault(o => o.OrderID == orderId);
            if (order != null)
            {
                order.IsShipped = true;
                _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
                return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
