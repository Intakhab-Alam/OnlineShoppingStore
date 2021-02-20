using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models.Home
{ 
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();
        public IPagedList<Tbl_Products> ListOfProducts { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search", search??(object)DBNull.Value)
            };
            IPagedList<Tbl_Products>  data = context.Database.SqlQuery<Tbl_Products>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel()
            {
                ListOfProducts = data
            };
        }
    }
}