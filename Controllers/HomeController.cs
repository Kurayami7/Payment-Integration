w﻿using Microsoft.AspNetCore.Mvc;
using Payment_Integration.Models;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;

namespace Payment_Integration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("create-checkout-session")]
        [HttpPost]
        public ActionResult Create()
        {
            var domain = "https://paymentintegration.azurewebsites.net/"; //replace with your local port number
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1OEuNsAln6MN0lfE8JbtUtjT",
                    Quantity = 1,
                  },

                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1OEuNEAln6MN0lfEQe4ytQUe",
                    Quantity = 3,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Home/Success",
                CancelUrl = domain + "/Home/cancel",

            };
            StripeConfiguration.ApiKey = "Insert your secret key/API here";
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult cancel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
