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

    [Route("brand")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetList()
        {
            var brands = await _brandService.GetAll();
            return Json(brands);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] BrandModel brand)
        {
            bool result = false;
            string message = "";
            try
            {
                if (brand.Id == 0)
                {
                    result = await _brandService.Create(brand);
                    message = result ? "Tạo thương hiệu thành công." : "Tạo thương hiệu thất bại.";
                }
                else
                {
                    result = await _brandService.Update(brand);
                    message = result ? "Cập nhật thương hiệu thành công." : "Cập nhật thương hiệu thất bại.";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { success = result, message, data = brand });
        }

        [HttpPost("delete/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool result = await _brandService.Delete(id);
            return Json(new { success = result ? "Xóa thương hiệu thành công." : "Xóa thương hiệu thất bại.", id });
        }
    }
}

