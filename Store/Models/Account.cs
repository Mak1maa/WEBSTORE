using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/* Модель логина, где будет логин и пароль, что бы легко передавать по всему коду,
   как с Product, Order и Cart*/

namespace Store.Models
{
    public class Account
    {
        [Required(ErrorMessage = "Необходимо ввести логин")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        public string Password { get; set; }
    }
}