using BaseBusiness.Model;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    [Route("supplier")]
    public class SupplierController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

    }
}
