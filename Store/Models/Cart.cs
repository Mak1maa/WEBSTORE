using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/**
 * Модель корзины.
 * Методы: 
 * Метод добавления в корзину продукта - AddItem(Product product, int quantity)
 * Метод изменения количества товара в корзине EditQ(Product product, int quantity) 
 **/

namespace Store.Models
{
    public class Cart
    {
        private List<CartLine> listCart = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine cartItem = listCart.
                Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

            if (cartItem == null)
            {
                listCart.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else 
            {
                cartItem.Quantity += quantity;
            }
        }

        // Удаление из корзины 
        public void RemoveCartItem(Product product) => listCart.
            RemoveAll(l => l.Product.ProductID == product.ProductID);

        // Чистка корзины 
        public void Clear() => listCart.Clear();

        // Получение корзины (гет)
        public IEnumerable<CartLine> Lines => listCart;

        // Подчет цены
        public decimal TotalPrice() => listCart.Sum(l => l.Product.Price * l.Quantity);

        public List<CartLine> GetCartLines() => listCart;

        public void EditQ(Product product, int quantity) 
        {
            listCart.Where(l => l.Product.ProductID == product.ProductID).
                FirstOrDefault().Quantity = quantity;
        }
    }

    // Элемент корзины, где будет храниться продукт и количество
    public class CartLine 
    {
        public int CartLineID { get; set; }

        public Product Product { get; set; }

        [RegularExpression(@"\d", ErrorMessage = "Необходимо ввести кол-во товара")]

        public int Quantity { get; set; }
    }
}