using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WheyStoreVN.Config;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string phone, string email, string pass, string confirm)
        {
            string message = "";
            var code = new char[8];
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            Regex regex = new Regex(@"0(3\d{8}|9\d{8})");

            using (var context = new WheystoreContext())
            {
                List<Account> accounts = context.Accounts.ToList();
                bool checkAcc = false;
                foreach (Account account in accounts)
                {
                    if (account.Email.Equals(email))
                    {
                        checkAcc = true;
                    }
                }
                for (int i = 0; i < code.Length; i++)
                {
                    code[i] = chars[random.Next(chars.Length)];
                }

                if (checkAcc == true)
                {
                    message = "Email đã tồn tại";
                }
                else
                {
                    if (regex.IsMatch(phone) == false)
                    {
                        message = "Sai định dạng số điện thoại";
                    }
                    else
                    {
                        if (!confirm.Equals(pass))
                        {
                            message = "Nhập lại mật khẩu không đúng";
                        }
                        else
                        {
                            Account acc = new Account
                            {
                                Email = email,
                                Password = pass,
                                EmailVerify = true,
                                Role = 1,
                                Status = false
                            };
                            context.Accounts.Add(acc);
                            context.SaveChanges();
                            var verify = new String(code);
                            new Mail().SendMail(email, "Password", verify);
                            HttpContext.Session.SetString("verify", verify);
                            return Redirect("/Register/Verify");
                        }
                    }
                }

            }
            ViewBag.messsage = message;
            return View();
        }

        [HttpGet]
        public IActionResult Verify()
        {
            if (HttpContext.Session.GetString("verify") != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }
        [HttpPost]
        public IActionResult Verify(string verify)
        {
            String mess = "";
            if (HttpContext.Session.GetString("verify") != null)
            {
                string check = HttpContext.Session.GetString("verify");
                if (verify.Equals(check))
                {
                    HttpContext.Session.Remove("verify");
                    ViewBag.mess = "Mời bạn đăng nhập";
                    return Redirect("/Login/Login");
                }
                else
                {
                    ViewBag.mess = "Mã xác minh sai !";
                    return View();
                }             
            }
            else
            {
                return Redirect("/Login/Login");
            }

        }
    }
}
