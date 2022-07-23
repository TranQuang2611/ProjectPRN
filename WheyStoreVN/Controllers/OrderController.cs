using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult AddOrder(int id)
        {
            List<Cart> listCart = new List<Cart>();
            Customer customer = new Customer();
            List<Shipper> shipper = new List<Shipper>();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("customer") != null)
                {
                    customer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("customer"));
                    if (HttpContext.Session.GetString("cart") != null)
                    {
                        listCart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                    }
                    else
                    {
                        return Redirect("/Home/Home");
                    }
                    using (var context = new WheystoreContext())
                    {
                        shipper = context.Shippers.ToList();
                    }
                }
            }
            else
            {
                return Redirect("/Login/Login");
            }
            ViewBag.total = id;
            ViewBag.customer = customer;
            ViewBag.shipper = shipper;
            return View(listCart);
        }

        [HttpPost]
        public IActionResult OrderSucess(string adress, int total, int ship)
        {

            List<Cart> listCart = new List<Cart>();
            Customer customer = new Customer();
            Order order = new Order();
            Savor savor = new Savor();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("customer") != null)
                {
                    customer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("customer"));
                    if (HttpContext.Session.GetString("cart") != null)
                    {
                        listCart = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                        using (var context = new WheystoreContext())
                        {
                            order = new Order()
                            {
                                CustomerId = customer.CustomerId,
                                OrderDate = DateTime.Now,
                                ShipperId = ship,
                                Location = adress,
                                Amount = total,
                                Status = false,
                                Description = ""
                            };
                            foreach (Cart cart in listCart)
                            {
                                savor = context.Savors.FirstOrDefault(x => x.SavorId == cart.Product.SavorId);
                                order.OrderDetails.Add(new OrderDetail
                                {
                                    DetailId = cart.Product.DetailId,
                                    SavorId = cart.Product.Savor.SavorId,
                                    Quantity = cart.Quantity,
                                    Description = ""
                                }
                                    );
                            }
                            context.Add(order);
                            context.SaveChanges();
                            HttpContext.Session.Remove("cart");
                        }

                    }
                    else
                    {
                        return Redirect("/Home/Home");
                    }
                }
            }
            else
            {
                return Redirect("/Login/Login");
            }
            return View();
        }
    }
}