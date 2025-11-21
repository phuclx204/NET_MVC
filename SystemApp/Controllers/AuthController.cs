using BaseBusiness.Model;
using SystemApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemApp.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
