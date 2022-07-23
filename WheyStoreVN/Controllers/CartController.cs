using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class CartController : Controller
    {
        public IActionResult AddCart(int did, int sid, int quantity)
        {
            List<Cart> carts = new List<Cart>();
            ProductSavor pro = new ProductSavor();
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            using (var context = new WheystoreContext())
            {
                pro = context.ProductSavors.Include(x => x.Savor).Include(x => x.Detail).FirstOrDefault(x => x.DetailId == did && x.SavorId == sid);
            }
            if (HttpContext.Session.GetString("cart") != null)
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                bool check = false;
                foreach (Cart cart in carts)
                {
                    if (cart.Product.DetailId == pro.DetailId && cart.Product.SavorId == pro.SavorId)
                    {
                        check = true;
                        cart.Quantity += quantity;
                    }
                }
                if (check == false)
                {
                    carts.Add(new Cart(pro, quantity));
                }
            }
            else
            {
                carts.Add(new Cart(pro, quantity));
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts, setting));
            return Redirect("/Cart/ShoppingCart");
        }

        public IActionResult DeleteCart(int did, int sid, int quantity)
        {
            List<Cart> carts = new List<Cart>();
            ProductSavor pro = new ProductSavor();
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            using (var context = new WheystoreContext())
            {
                pro = context.ProductSavors.Include(x => x.Savor).Include(x => x.Detail).FirstOrDefault(x => x.DetailId == did && x.SavorId == sid);
            }
            if (HttpContext.Session.GetString("cart") != null)
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                foreach (Cart cart in carts)
                {
                    if (cart.Product.DetailId == pro.DetailId && cart.Product.SavorId == pro.SavorId)
                    {
                        cart.Quantity--;
                        if (cart.Quantity == 0)
                        {
                            carts.Remove(cart);
                        }
                        break;
                    }                 
                }
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts, setting));
            return Redirect("/Cart/ShoppingCart");
        }

        public IActionResult RemoveCart(int did, int sid)
        {
            List<Cart> carts = new List<Cart>();
            ProductSavor pro = new ProductSavor();
            var setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            using (var context = new WheystoreContext())
            {
                pro = context.ProductSavors.Include(x => x.Savor).Include(x => x.Detail).FirstOrDefault(x => x.DetailId == did && x.SavorId == sid);
            }
            if (HttpContext.Session.GetString("cart") != null)
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(HttpContext.Session.GetString("cart"));
                foreach (Cart cart in carts)
                {
                    if (cart.Product.DetailId == pro.DetailId && cart.Product.SavorId == pro.SavorId)
                    {
                        carts.Remove(cart);
                        break;
                    }
                }
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(carts, setting));
            return Redirect("/Cart/ShoppingCart");
        }

        public IActionResult ShoppingCart()
        {
            List<Cart> carts = new List<Cart>();
            string s = HttpContext.Session.GetString("cart");
            if(s != null)
            {
                carts = JsonConvert.DeserializeObject<List<Cart>>(s);
            }              
            return View(carts);
        }
    }
}
