using BaseBusiness.Model;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Route("brand")]
    public class BrandController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

    }
}
