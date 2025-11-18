using Microsoft.AspNetCore.Mvc;
using BaseBusiness.util;
using BaseBusiness.Model;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Catalog.Controllers
{
    public class ProductController : Controller
    {
        // Nhận tham số 'keyword' từ thanh tìm kiếm trên View
        public IActionResult Index(string keyword = "")
        {
            string sql = "SELECT * FROM Product";
            List<SqlParameter> paramList = new List<SqlParameter>();

            // Nếu có từ khóa thì nối thêm điều kiện WHERE
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " WHERE Name LIKE @Keyword OR Code LIKE @Keyword";
                paramList.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
            }

            sql += " ORDER BY ID DESC";

            // Gọi DBUtils với tham số
            List<ProductModel> list = DBUtils.GetList<ProductModel>(sql, paramList.ToArray());

            // Truyền lại từ khóa ra View để giữ trên ô tìm kiếm
            ViewBag.Keyword = keyword;

            return View(list);
        }
    }
}