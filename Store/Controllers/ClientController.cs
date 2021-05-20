using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;
using System.Data.SqlClient;
using System.Data;

/**
 * Контроллер клиента. 
 * Методы:
 * Метод вызываемый из представления ClientForm работает по Post, принимает заказ (Client) - ClientForm()
 * Метод который возвращает представление OrderView (для админа) - public ViewResult OrderView() => View(repo.GetOrdersRepository()); М
 * Метод который вызывает ClientForm, работает по GET, когда верхний метод только по Post - public ViewResult OrderForm() => View(); 
**/

namespace Store.Controllers
{
    public class ClientController : Controller
    {
        private ClientRepository repository = new ClientRepository();

        [HttpPost]
        public ViewResult ClientForm(Client client) 
        {
            if (ModelState.IsValid)
            {
                int clientNum = repository.GetClientsRepository().Last().ClientNum + 1;

                Cart cart = (Cart)HttpContext.Session["Cart"];

                if (cart == null) 
                {
                    ModelState.AddModelError("", "Корзина пуста");
                    return View();
                }
                else
                {
                    client.products = cart.GetCartLines();

                    string str = System.Configuration.ConfigurationManager.
                        ConnectionStrings["StoreDB"].ConnectionString; 

                    using (SqlConnection con = new SqlConnection(str))
                    {
                        for (int i = 0; i < client.products.Count; i++)
                        {
                            SqlCommand com = new SqlCommand("INSERT INTO Clients " +
                                "(ProductID, Name, Surname, Patronymic, Phone, Email, " +
                                "Quantity, ClientNumber) " +
                            "VALUES (@ProductID, @Name, @Surname, @Patronymic, @Phone, @Email, " +
                                "@Quantity, @ClientNumber)", con);

                            com.Parameters.Add("@ProductID", SqlDbType.Int).
                                Value = client.products[i].Product.ProductID;

                            com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = client.Name;

                            com.Parameters.Add("@Surname", SqlDbType.NVarChar).Value = client.Surname;

                            com.Parameters.Add("@Patronymic", SqlDbType.NVarChar).Value = client.Patronymic;

                            com.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = client.Phone;

                            com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = client.Email;

                            com.Parameters.Add("@Quantity", SqlDbType.Int).
                                Value = client.products[i].Quantity;

                            com.Parameters.Add("@ClientNumber", SqlDbType.Int).
                                Value = clientNum;

                            con.Open();

                            com.ExecuteNonQuery();

                            con.Close();
                        }
                    }
                    return View("Thanks", client);
                }
            }
            else{ return View(); }
        }

        [Authorize]
        public ViewResult ClientView() => View(repository.GetClientsRepository());

        [HttpGet]
        public ViewResult ClientForm() => View();
    }
}