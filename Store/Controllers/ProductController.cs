using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Models;

/**
 * Контроллер
 * Конструктор контроллера, как только идет вызов /Product/... создается экземпляр контроллера, соответственно работает этот конструтор - public ProductController()
 * Создаем экземлпяр хранилища при каждом создании контроллера, то есть при каждом запуске страницы
 * 
 * Вызов представления главной страницы где все продукты. В представление передаем тот самый лист продуктов, который берем в экземпляре класса ProductRepository где идет обращение к бд - public ViewResult Index() => View(repo.GetRepository());
 * 
 **/

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository repository;

        public ProductController()
        {
            repository = new ProductRepository(); 
        }

        public ViewResult Index() => View(repository.GetRepository());
    }
}