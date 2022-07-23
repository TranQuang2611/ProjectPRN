using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            string account = HttpContext.Session.GetString("account");
            if (account == null)
            {
                return View();
            }
            else
            {
                return Redirect("/Home/Home");
            }

        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            Account account = new Account();
            Customer customer = new Customer();
            Admin admin = new Admin();
            using (var context = new WheystoreContext())
            {
                account = context.Accounts.Include(x => x.Customer).FirstOrDefault(x => x.Email.Equals(username) && x.Password.Equals(password));
                if (account != null)
                {
                    HttpContext.Session.SetString("account", JsonConvert.SerializeObject(account,setting));
                    if (account.Role == 1)
                    {
                        if (account.Status == true)
                        {
                            customer = context.Customers.FirstOrDefault(x => x.CustomerId == account.AccountId);
                            HttpContext.Session.SetString("customer", JsonConvert.SerializeObject(customer, setting));
                            return Redirect("/Home/Home");
                        }
                        else
                        {
                            return Redirect("/Register/Register");
                        }                       
                    }
                    else
                    {
                        admin = context.Admins.FirstOrDefault(x => x.AdminId == account.AccountId);
                        HttpContext.Session.SetString("admin", JsonConvert.SerializeObject(admin, setting));
                        return Redirect("/Admin/AdminHome");
                    }
                    
                }
                else
                {
                    string mess = "Tài khoản không tồn tại !!!";
                    ViewBag.mess = mess;
                    return View();
                }
            }
        }
    }
}
