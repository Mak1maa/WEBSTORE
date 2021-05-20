using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public static class Identify
    {
        private static string adminName = "admin";
        private static string adminPassword = "12345";

        public static bool LogIn(Account account) // Метод сверяет данные из модели Account с полями выше
        {
            if (account.UserName == adminName && account.Password == adminPassword){ return true; }
            else{ return false; }
        }
    }
}