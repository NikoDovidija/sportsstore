using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStoreCore.Models;

namespace SportsStoreCore.Controllers
{
    public class OrderController : Controller
    {

        IOrderRepository orderRepository;
        Cart mcart;
        public OrderController(IOrderRepository repository,Cart cart)
        {
            mcart = cart;
            orderRepository = repository;
        }
        public ViewResult Checkout() => View(new Order());

        [Authorize]
        public ViewResult List() =>
            View(orderRepository.Orders.Where(o => !o.Shipped));
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = orderRepository.Orders
            .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                orderRepository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (mcart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = mcart.Lines.ToArray();
                orderRepository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            mcart.Clear();
            return View();
        }
    }
}