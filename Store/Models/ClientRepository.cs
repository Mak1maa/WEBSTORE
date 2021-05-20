using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

/**
 * Модель, где можно получить клиентов (заказы) из базы
 * 
**/

namespace Store.Models
{
    public class ClientRepository
    {
        private List<Client> clientsRepository;

        private ProductRepository repository;

        public ClientRepository()
        {
            clientsRepository = new List<Client>();
            repository = new ProductRepository();
            FillClientRepository();
        }

        public IEnumerable<Client> GetClientsRepository() => clientsRepository;

        public void FillClientRepository()
        {
            int oldClientNum = 0;
            string str = System.Configuration.ConfigurationManager.
                ConnectionStrings["StoreDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clients", con);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int newClientNum = (int)r["ClientNumber"];

                    if (oldClientNum != newClientNum || oldClientNum == 0)
                    {
                        Client p = new Client();
                        p.ClientID = (int)r["Id"];
                        p.Name = r["Name"].ToString();
                        p.Surname = r["Surname"].ToString();
                        p.Patronymic = r["Patronymic"].ToString();
                        p.Email = r["Email"].ToString();
                        p.Phone = r["Phone"].ToString();

                        CartLine cartItem = new CartLine()
                        {
                            Quantity = (int)r["Quantity"],
                            Product = repository.GetRepository().
                            FirstOrDefault(c => c.ProductID == (int)r["ProductID"])
                        };

                        p.ClientNum = newClientNum;
                        p.products = new List<CartLine>() { cartItem };
                        clientsRepository.Add(p);
                        oldClientNum = p.ClientNum;
                    }
                    else if (oldClientNum == newClientNum)
                    {
                        CartLine cartItem = new CartLine()
                        {
                            Quantity = (int)r["Quantity"],
                            Product = repository.GetRepository().
                            FirstOrDefault(c => c.ProductID == (int)r["ProductID"])
                        };

                        clientsRepository.Last().products.Add(cartItem);
                    }
                }
            }
        }
    }
}