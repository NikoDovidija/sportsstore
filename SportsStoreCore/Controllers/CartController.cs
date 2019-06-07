﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStoreCore.Infrastructure;
using SportsStoreCore.Models;
using SportsStoreCore.Models.ViewModels;

namespace SportsStoreCore.Controllers
{
    public class CartController : Controller
    {

        private IProductRepository myRepository;
        public CartController(IProductRepository repository)
        {
            myRepository = repository;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = myRepository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,
        string returnUrl)
        {
            Product product = myRepository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }


        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
    }
}