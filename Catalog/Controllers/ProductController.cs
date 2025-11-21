using Microsoft.AspNetCore.Mvc;
using BaseBusiness.util;
using BaseBusiness.Model;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Catalog.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}