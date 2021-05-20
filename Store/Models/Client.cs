using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/* Модель клиента. ФИО, телефон, почта, номер заказа. */

namespace Store.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Необходимо ввести свою Фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Необходимо ввести своё Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо ввести своё Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Необходимо ввести номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Необходимо ввести электронную почту")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Некорректная электронная почта")]
        public string Email { get; set; }

        // Лист элементов корзины, который мы заполняем в контроллере
        public List<CartLine> products { get; set; } 
        public int ClientNum { get; set; }
    }
}