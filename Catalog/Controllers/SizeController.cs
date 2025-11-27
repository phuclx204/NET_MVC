using BaseBusiness.Model;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Catalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [Authorize]

    [Route("size")]
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("form")]
        public IActionResult Form(long id = 0)
        {
            ViewBag.Id = id;
            return View("Save");
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetList()
        {
            var sizes = await _sizeService.GetAll();
            return Json(new { success = true, data = sizes });
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SizeModel size)
        {
            bool result = false;
            string message = "";
            try
            {
                if (size.Id == 0)
                {
                    result = await _sizeService.Create(size);
                    message = result ? "Tạo kích thước thành công." : "Tạo kích thước thất bại.";
                }
                else
                {
                    result = await _sizeService.Update(size);
                    message = result ? "Cập nhật kích thước thành công." : "Cập nhật kích thước thất bại.";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { success = result, message = message });
        }

        [HttpPost("delete/{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _sizeService.Delete(id);
            return Json(new
            {
                success = result,
                message = result ? "Xóa thành công." : "Xóa thất bại"
            });
        }


        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(long id)
        {
            var item = await _sizeService.GetById(id);
            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy dữ liệu" });

            return Json(new { success = true, data = item });
        }
    }
}
