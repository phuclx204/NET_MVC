using BaseBusiness.Model;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Authorize]

    [Route("category")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
