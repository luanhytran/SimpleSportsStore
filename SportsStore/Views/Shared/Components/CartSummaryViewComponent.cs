using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Views.Shared.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        /* This view component take advantage of the service that receive a Cart object 
         * as a constructor argument */
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
