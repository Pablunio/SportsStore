using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {

        #region Constructor
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            this.repository = repo;
        }
        #endregion

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = getCart(), ReturnUrl = returnUrl });
        }

        #region Redirect Actions
        public RedirectToRouteResult AddToCart(int productID, string returnURL)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            /// add to cart
            /// 
            if (product != null)
                getCart().AddItem(product, 1);
            ///redirect
            ///
            return RedirectToAction("Index", new { returnURL });
        }

        public RedirectToRouteResult RemoveFromCart(int productID, string returnURL)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            /// add to cart
            /// 
            if (product != null)
                getCart().RemoveLine(product);
            ///redirect
            ///
            return RedirectToAction("Index", new { returnURL });

        }
        #endregion

        #region Helper functions
        private Cart getCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        } 
        #endregion
    }
}