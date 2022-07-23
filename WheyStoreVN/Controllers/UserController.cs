using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Favorite()
        {
            List<FavoriteProduct> listFavorite = new List<FavoriteProduct>();
            Customer customer = new Customer();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("customer") != null)
                {
                    customer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("customer"));
                    using (var context = new WheystoreContext())
                    {
                        listFavorite = context.FavoriteProducts.Include(x => x.Detail).Where(x => x.CustomerId == customer.CustomerId).ToList();
                    }
                }
            }
            else
            {
                return Redirect("/Login/Login");
            }
            return View(listFavorite);
        }

        public IActionResult AddFavorite(int id, string url)
        {
            ProductDetail pro = new ProductDetail();
            Customer customer = new Customer();
            List<FavoriteProduct> listFavorite = new List<FavoriteProduct>();
            if (HttpContext.Session.GetString("account") != null)
            {
                if (HttpContext.Session.GetString("customer") != null)
                {
                    customer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("customer"));
                    using (var context = new WheystoreContext())
                    {
                        pro = context.ProductDetails.FirstOrDefault(x => x.DetailId == id);
                        listFavorite = context.FavoriteProducts.Where(x => x.CustomerId == customer.CustomerId).ToList();
                        int k = listFavorite.Count;
                        bool exist = false;
                        foreach (var product in listFavorite)
                        {
                            if (product.DetailId == pro.DetailId)
                            {
                                exist = true;
                            }
                        }
                        if (exist == false)
                        {
                            FavoriteProduct favoriteProduct = new FavoriteProduct() { CustomerId = customer.CustomerId, DetailId = pro.DetailId };
                            context.FavoriteProducts.Add(favoriteProduct);
                            context.SaveChanges();
                            return Redirect(url);
                        }
                    }
                }
                return Redirect(url);
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public IActionResult DeleteFavorite(int id)
        {
            FavoriteProduct product = new FavoriteProduct();
            Customer customer = new Customer();
            if (HttpContext.Session.GetString("customer") != null)
            {
                customer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("customer"));
                using (var context = new WheystoreContext())
                {                  
                    product = context.FavoriteProducts.FirstOrDefault(x => x.DetailId == id && x.CustomerId == customer.CustomerId);
                    context.FavoriteProducts.Remove(product);
                    context.SaveChanges();
                }               
            }
            return Redirect("/User/Favorite");
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("account") != null)
            {
                HttpContext.Session.Remove("customer");
                HttpContext.Session.Remove("account");
            }
            return Redirect("/Home/Home");
        }
    }
}
