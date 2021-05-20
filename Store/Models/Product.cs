using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

/* Модель продуктов. Их название, цена, описание, кол-во. */

namespace Store.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        //[Display(Name = "Название")]
        [Required(ErrorMessage = "Необходимо указать название товара")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        //[Display(Name = "Описание")]
        [Required(ErrorMessage = "Необходимо указать описание товара")]
        public string Description { get; set; }

        //[Display(Name = "Цена (руб)")]
        [Required(ErrorMessage = "Необходимо указать цену товара")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, " +
            "указать положительное значение для цены")]
        [RegularExpression(@"\d+", ErrorMessage = "Пожалуйста, укажите цену товара")]
        public decimal Price { get; set; }

        //[Display(Name = "Кол-во")]
        [Required(ErrorMessage = "Необходимо указать кол-во для товара")]
        [RegularExpression(@"\d+", ErrorMessage = "Пожалуйста, укажите кол-во товара")]
        public int Quantity { get; set; }
    }
}