using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WheyStoreVN.Models;

namespace WheyStoreVN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Home()
        {
            List<OrderDetail> top3WheyOrder = new List<OrderDetail>();
            List<ProductDetail> top3Whey = new List<ProductDetail>();
            List<OrderDetail> top3MassOrder = new List<OrderDetail>();
            List<ProductDetail> top3Mass = new List<ProductDetail>();
            List<ProductDetail> top3New = new List<ProductDetail>();
            List<Product> top3Pro = new List<Product>();
            using (var context = new WheystoreContext())
            {
                top3WheyOrder = context.OrderDetails.Where(x => x.Detail.Product.SubCat.CatId == 2)
                                .GroupBy(x => x.DetailId)
                                .Select(x => new OrderDetail
                                {
                                    DetailId = x.Key,
                                    Quantity = x.Sum(y => y.Quantity),
                                }).OrderByDescending(x => x.Quantity).Take(3).ToList();
                top3Whey = context.ProductDetails.ToList();
                top3Whey = top3Whey.Where(x => top3WheyOrder.Any(y => y.DetailId == x.DetailId)).ToList();

                top3MassOrder = context.OrderDetails.Where(x => x.Detail.Product.SubCat.CatId == 1)
                                .GroupBy(x => x.DetailId)
                                .Select(x => new OrderDetail
                                {
                                    DetailId = x.Key,
                                    Quantity = x.Sum(y => y.Quantity),
                                }).OrderByDescending(x => x.Quantity).Take(3).ToList();
                top3Mass = context.ProductDetails.ToList();
                top3Mass = top3Mass.Where(x => top3MassOrder.Any(y => y.DetailId == x.DetailId)).ToList();

                top3Pro = context.Products.OrderByDescending(x => x.ProductId).Take(3).ToList();
                top3New = context.ProductDetails.ToList();
                top3New = top3New.Where(x => top3Pro.Any(y => y.ProductId == x.ProductId)).ToList();
            }
            ViewBag.mass = top3Mass;
            ViewBag.newpro = top3New;
            return View(top3Whey);
        }


        public IActionResult ShopMass(int id, int[] brand, int[] size, int[] savor, int[] country, int price)
        {
            Category category = new Category();
            SubCategory subCategory = new SubCategory();
            List<SubCategory> subCatList = new List<SubCategory>();
            List<ProductDetail> productDetailList = new List<ProductDetail>();
            List<Brand> brandList = new List<Brand>();
            List<Size> sizeList = new List<Size>();
            List<Country> countryList = new List<Country>();
            List<Savor> savorList = new List<Savor>();
            using (var context = new WheystoreContext())
            {
                if (id == 0)
                {
                    productDetailList = context.ProductDetails.Include(x => x.Product).Include(x => x.ProductSavors).Where(x => x.Product.SubCat.CatId == 1).ToList();
                    category = context.Categories.Find(1);

                }
                else
                {
                    productDetailList = context.ProductDetails.Where(x => x.Product.SubCatId == id).ToList();
                    subCategory = context.SubCategories.Find(id);
                }
                if (brand.Length > 0)
                {
                    List<Product> productList = context.Products.ToList();
                    productList = productList.Where(x => brand.Any(y => x.BrandId == y)).ToList();
                    productDetailList = productDetailList.Where(x => productList.Any(y => y == x.Product)).ToList();
                }
                if (size.Length > 0)
                {
                    productDetailList = productDetailList.Where(x => size.Any(y => y == x.SizeId)).ToList();
                }
                if (savor.Length > 0)
                {
                    List<ProductSavor> productSavor = context.ProductSavors.Where(x => savor.Any(y => y == x.SavorId)).ToList();
                    productDetailList = productDetailList.Where((x) => productSavor.Any(y => y.DetailId == x.DetailId)).ToList();
                }
                if (country.Length > 0)
                {
                    List<Product> productList = context.Products.ToList();
                    productList = productList.Where(x => country.Any(y => y == x.CountryId)).ToList();
                    productDetailList = productDetailList.Where(x => productList.Any(y => y == x.Product)).ToList();
                }
                if (price != 0)
                {
                    productDetailList = productDetailList.Where(x => x.SellPrice <= price).ToList();
                }
                brandList = context.Brands.ToList();
                sizeList = context.Sizes.ToList();
                countryList = context.Countries.ToList();
                savorList = context.Savors.ToList();
            }
            ViewBag.cat = category.Description;
            ViewBag.subcat = subCategory.Description;
            ViewBag.Id = id;
            ViewBag.brand = brandList;
            ViewBag.size = sizeList;
            ViewBag.country = countryList;
            ViewBag.savor = savorList;
            ViewBag.checkBrand = brand;
            ViewBag.checkSize = size;
            ViewBag.checkSavor = savor;
            ViewBag.checkCountry = country;
            ViewBag.checkPrice = price;
            return View(productDetailList);
        }

        public IActionResult ShopWhey(int id, int[] brand, int[] size, int[] savor, int[] country, int price)
        {
            Category category = new Category();
            SubCategory subCategory = new SubCategory();
            List<SubCategory> subCatList = new List<SubCategory>();
            List<ProductDetail> productDetailList = new List<ProductDetail>();
            List<Brand> brandList = new List<Brand>();
            List<Size> sizeList = new List<Size>();
            List<Country> countryList = new List<Country>();
            List<Savor> savorList = new List<Savor>();
            using (var context = new WheystoreContext())
            {
                if (id == 0)
                {
                    productDetailList = context.ProductDetails.Where(x => x.Product.SubCat.CatId == 2).ToList();
                    category = context.Categories.Find(2);
                }
                else
                {
                    productDetailList = context.ProductDetails.Where(x => x.Product.SubCatId == id).ToList();
                    subCategory = context.SubCategories.Find(id);
                }
                if (brand.Length > 0)
                {
                    List<Product> productList = context.Products.ToList();
                    productList = productList.Where(x => brand.Any(y => x.BrandId == y)).ToList();
                    productDetailList = productDetailList.Where(x => productList.Any(y => y == x.Product)).ToList();
                }
                if (size.Length > 0)
                {
                    productDetailList = productDetailList.Where(x => size.Any(y => y == x.SizeId)).ToList();
                }
                if (savor.Length > 0)
                {
                    List<ProductSavor> productSavor = context.ProductSavors.Where(x => savor.Any(y => y == x.SavorId)).ToList();
                    productDetailList = productDetailList.Where((x) => productSavor.Any(y => y.DetailId == x.DetailId)).ToList();
                }
                if (country.Length > 0)
                {
                    List<Product> productList = context.Products.ToList();
                    productList = productList.Where(x => country.Any(y => y == x.CountryId)).ToList();
                    productDetailList = productDetailList.Where(x => productList.Any(y => y == x.Product)).ToList();
                }
                if (price != 0)
                {
                    productDetailList = productDetailList.Where(x => x.SellPrice <= price).ToList();
                }
                brandList = context.Brands.ToList();
                sizeList = context.Sizes.ToList();
                countryList = context.Countries.ToList();
                savorList = context.Savors.ToList();
            }
            ViewBag.cat = category.Description;
            ViewBag.subcat = subCategory.Description;
            ViewBag.Id = id;
            ViewBag.brand = brandList;
            ViewBag.size = sizeList;
            ViewBag.country = countryList;
            ViewBag.savor = savorList;
            ViewBag.checkBrand = brand;
            ViewBag.checkSize = size;
            ViewBag.checkSavor = savor;
            ViewBag.checkCountry = country;
            ViewBag.checkPrice = price;
            return View(productDetailList);
        }

        

    }
}
