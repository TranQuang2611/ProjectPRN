using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminHome()
        {
            Admin admin = new Admin();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("admin") != null)
                {
                    List<Order> order = new List<Order>();
                    using (var context = new WheystoreContext())
                    {
                        order = context.Orders.Where(x => x.Status == false).ToList();
                    }
                    return View(order);
                }
                else
                {
                    return Redirect("/Home/Home");
                }

            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public IActionResult WaitOrderDetail(int id)
        {
            Admin admin = new Admin();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("admin") != null)
                {
                    List<OrderDetail> orderDetail = new List<OrderDetail>();
                    List<ProductSavor> productSavors = new List<ProductSavor>();
                    Order order = new Order();
                    using (var context = new WheystoreContext())
                    {
                        order = context.Orders.Include(x => x.Customer).Include(x=> x.Shipper).FirstOrDefault(x => x.OrderId == id);
                        orderDetail = context.OrderDetails.Include(x => x.Detail).Include(x => x.Order).Include(x => x.Savor).Where(x => x.OrderId == id).ToList();

                        productSavors = context.ProductSavors.ToList();
                        productSavors = productSavors.Where(x => orderDetail.Any(y => y.DetailId == x.DetailId && y.SavorId == x.SavorId)).ToList();

                        var check = from t1 in orderDetail
                                    join t2 in productSavors
                                    on t1.DetailId equals t2.DetailId
                                    where t1.SavorId == t2.SavorId
                                    select new
                                    {
                                        proID = t1.DetailId,
                                        name = t2.Detail.Name,
                                        img = t2.Detail.Img,
                                        savor = t1.SavorId,
                                        sell = t2.Detail.SellPrice,
                                        inPrice = t2.Detail.DefaultPrice,
                                        buy = t1.Quantity,
                                        instock = t2.Quantity,
                                        profit = t2.Detail.SellPrice - t2.Detail.DefaultPrice
                                    };                   
                        ViewBag.check = check;
                    }
                    ViewBag.savor = productSavors;
                    ViewBag.order = order;                   
                    return View(orderDetail);
                }
                else
                {
                    return Redirect("/Home/Home");
                }

            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public IActionResult ApproveProduct()
        {
            Admin admin = new Admin();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("admin") != null)
                {
                    List<Order> order = new List<Order>();
                    using (var context = new WheystoreContext())
                    {
                        order = context.Orders.Where(x => x.OrderDetails.Count > 0).ToList();
                    }
                    return View(order);
                }
                else
                {
                    return Redirect("/Home/Home");
                }

            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public IActionResult RejectProduct()
        {
            Admin admin = new Admin();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("admin") != null)
                {
                    List<Order> order = new List<Order>();
                    using (var context = new WheystoreContext())
                    {
                        order = context.Orders.Where(x => x.OrderDetails.Count == 0).ToList();
                    }
                    return View(order);
                }
                else
                {
                    return Redirect("/Home/Home");
                }

            }
            else
            {
                return Redirect("/Login/Login");
            }
        }
    }
}
