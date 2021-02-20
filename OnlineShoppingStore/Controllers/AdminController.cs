using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;

            if(categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }

            return View("UpdateCategory", cd);
        }
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Products>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Products>().GetFirstOrDefault(productId));
        }
        [HttpPost]
        public ActionResult ProductEdit(Tbl_Products tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), pic);
                //file is uploaded
                file.SaveAs(path);
            }
            else
            {
                //var pId = tbl.ProductId;
                //var product = _unitOfWork.GetRepositoryInstance<Tbl_Products>().GetFirstOrDefault(pId);
                pic = tbl.ProductImage;
            }
            tbl.ProductImage = pic;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Products>().Update(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Tbl_Products tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if(file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), pic);
                //file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Products>().Add(tbl);
            return RedirectToAction("Product");
        }

    }
}