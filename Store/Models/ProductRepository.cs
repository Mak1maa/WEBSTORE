using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Controllers;
using Store.Models;
using System.Data.SqlClient;

/**
 * Модель хранилища продуктов, отделено от контроллера, из-зи того, что этот класс используется по всему коду
 * Метод, который возвращает лист продуктов - public IEnumerable<Product> GetRepository() => product;
**/

namespace Store.Models
{
    public class ProductRepository
    {
        public IEnumerable<Product> GetRepository() => product;

        private List<Product> product;

        public ProductRepository() 
        {
            product = new List<Product>(); 
            Repository();
        }

        private void Repository()
        {
            string str = System.Configuration.ConfigurationManager.
                ConnectionStrings["StoreDB"].ConnectionString; 

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Products", con);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    Product p = new Product
                    {
                        ProductID = (int)r["Id"],
                        Name = r["Name"].ToString(),
                        Description = r["Description"].ToString(),
                        Price = (decimal)r["Price"],
                        Quantity = (int)r["Quantity"],
                    };
                    product.Add(p);
                }
            }
        }
    }
}