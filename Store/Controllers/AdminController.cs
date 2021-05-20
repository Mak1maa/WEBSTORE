using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;

/**
 * Контроллер администратор. 
 * Методы:
 * Метод вызывается из представления по нажатию кнопки - Добавить продукт, принимает продукт - AddProduct()
 * Метод принимает id продукта и количество товара, вызывается по нажатию кнопки: Изменить количество товара в представлении - EditProductView EditProduct()
 * Метод вызывающий представление Account, где форма заполнения логина и пароля - Account()
 * Метод выхода из админки, на главной админской странице есть кнопка выйти - LogOut()
 **/

namespace Store.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProductRepository repository = new ProductRepository(); 

        public ViewResult EditProductView() => View(repository.GetRepository()); 


        public ViewResult AddProductForm() => View();

        [HttpPost]
        public ViewResult AddProduct(Product product) 
        {
            if (ModelState.IsValid)
            {
                string str = System.Configuration.ConfigurationManager.
                    ConnectionStrings["StoreDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlCommand com = new SqlCommand("INSERT INTO Products (Name, Description, Quantity, " +
                        "Price) " +
                    "VALUES(@Name, @Description, @Quantity, @Price)", con);

                    com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;

                    com.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;

                    com.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;

                    com.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;

                    con.Open();

                    com.ExecuteNonQuery();

                    con.Close();
                }
                return View("AddProduct", product);
            }
            else
            {
                return View("AddProductForm");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(int productId, int quantity) 
        {
            if (ModelState.IsValid)
            {
                string str = System.Configuration.ConfigurationManager.
                    ConnectionStrings["StoreDB"].ConnectionString;

                if (quantity >= 0)
                {
                    using (SqlConnection con = new SqlConnection(str))
                    {
                        SqlCommand com = new SqlCommand("UPDATE Products SET Quantity = @Quantity" +
                            " WHERE Id = @ProductID", con);

                        com.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;

                        com.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;

                        con.Open();

                        com.ExecuteNonQuery();

                        con.Close();
                    }
                }
                    return RedirectToAction("EditProductView", "Admin");
                } else { 
                    return View(); 
                }
        }

        [AllowAnonymous] 
        public ViewResult Account() => View(); 

        [AllowAnonymous] 
        [HttpPost]
        public ActionResult Account(Account account)
        {
            if (ModelState.IsValid)
            {
                if (Identify.LogIn(account)) 
                {
                    FormsAuthentication.SetAuthCookie(account.UserName, true); 
                    return RedirectToAction("Index", "Admin");
                }
                else 
                {
                    ModelState.AddModelError("", "Некорректные данные");
                }
            }
            return View(account);

        }

        [HttpPost]
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Product"); 
        }

        public ViewResult Index() => View();
    }
}