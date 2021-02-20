using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        dbMyOnlineShoppingEntities ctx = new dbMyOnlineShoppingEntities();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult CheckOutDetails()
        {
            return View();
        }

        public ActionResult AddToCart(int productId)
        {
            if(Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Products.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Products.Find(productId);
                foreach (var item in cart.ToList())
                {
                    if(item.Product.ProductId==productId)
                    {
                        int preQty = item.Quantity;
                        cart.Remove(item);
                        //cart.RemoveAll(x=> item.Product.ProductId == productId);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = preQty+1
                        });
                        //break;
                    }
                    else
                    {
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        
                    }
                    //break;
                }
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = ctx.Tbl_Products.Find(productId);
            foreach (var item in cart)
            {
                if(item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}