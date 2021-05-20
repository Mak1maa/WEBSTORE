using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;

/**
 * Контроллер корзины. 
 * Методы:
 * Метод добавления продукта в корзину, срабатывает из представления при нажатии на кнопку "Добавить корзину" -  AddToCart()
 * Метод удаления из корзины, срабатывает при нажатии на кнопку "Удалить из корзины" - RemoveFromCart()
 * Метод изменения кол-ва товара в корзине, срабатывает при нажатии на кнопку "Изменить кол-во товара" - Edit()
 * Метод получения корзины из сессии - GetCart()
 * Метод установки корзину в сессию - SetCart()
**/

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private ProductRepository repository;

        public CartController()
        {
            repository = new ProductRepository();
        }

        [HttpPost]
        public RedirectToRouteResult AddToCart(int productId)
        {
            Product product = repository.GetRepository().
                FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SetCart(cart);
            }

            return RedirectToAction("Index", "Product");
        }

        public RedirectToRouteResult RemoveFromCart(int productId) 
        {
            Product product = repository.GetRepository().
                FirstOrDefault(p => p.ProductID == productId);

            if (product != null) 
            {
                Cart cart = GetCart(); 
                cart.RemoveCartItem(product);
                SetCart(cart);
            }

            return RedirectToAction("Cart", "Cart"); 
        }

        public RedirectToRouteResult Edit(int productId, int quantity) 
        {
            Product product = repository.GetRepository().
                FirstOrDefault(p => p.ProductID == productId);

            if (product != null && product.Quantity >= quantity && quantity != 0 && quantity > 0) 
            {
                Cart cart = GetCart();
                cart.EditQ(product, quantity);
                SetCart(cart); 
            }

            return RedirectToAction("Cart", "Cart"); 
        }

        private Cart GetCart()
        {
            Cart cart;

            if (HttpContext.Session["Cart"] != null){ cart = (Cart)HttpContext.Session["Cart"]; }
            else { cart = new Cart(); }

            return cart;
        }

        private void SetCart(Cart cart) { HttpContext.Session.Add("Cart", cart);}

        public ViewResult Cart() => View(GetCart());
    }
}