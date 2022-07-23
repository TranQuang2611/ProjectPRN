using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult ProductDetail(int pid, int dtid)
        {
            Product product = new Product();
            ProductDetail productDetail = new ProductDetail();
            List<ProductSavor> productSavor = new List<ProductSavor>();
            List<ProductDetail> listProduct = new List<ProductDetail>();
            int? total = 0;
            using (var context = new WheystoreContext())
            {
                product = context.Products.FirstOrDefault(p => p.ProductId == pid);
                total = context.ProductSavors.Where(x => x.DetailId == dtid).Sum(x => x.Quantity);
                listProduct = context.ProductDetails.Where(x => x.ProductId == pid).ToList();
                productDetail = context.ProductDetails.Include(x => x.Product.Brand).Include(x => x.Size).Include(x => x.Product.Country).Where(x => x.DetailId == dtid).FirstOrDefault();
                productSavor = context.ProductSavors.Include(x => x.Savor).Where(x => x.DetailId == dtid).ToList();
            }
            ViewBag.countProduct = total;
            ViewBag.listSavor = productSavor;
            ViewBag.listProduct = listProduct;
            ViewBag.pid = pid;
            ViewBag.product = product.Description;
            return View(productDetail);
        }

        public string getQuantity(int sid, int did)
        {
            using (var context = new WheystoreContext())
            {
                return context.ProductSavors.FirstOrDefault(x => x.DetailId == did && x.SavorId == sid).Quantity.ToString();
            }
                
        }
    }
}
