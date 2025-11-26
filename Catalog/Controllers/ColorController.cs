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
    [Route("color")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
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
            var colors = await _colorService.GetAll();
            return Json(new { success = true, data = colors });
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] ColorModel color)
        {
            bool result = false;
            string message = "";
            try
            {
                if (color.Id == 0)
                {
                    result = await _colorService.Create(color);
                    message = result ? "Tạo màu sắc thành công." : "Tạo màu sắc thất bại.";
                }
                else
                {
                    result = await _colorService.Update(color);
                    message = result ? "Cập nhật màu sắc thành công." : "Cập nhật màu sắc thất bại.";
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
            var result = await _colorService.Delete(id);
            return Json(new
            {
                success = result,
                message = result ? "Xóa màu sắc thành công." : "Xóa thất bại"
            });
        }



        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetail(long id)
        {
            var item = await _colorService.GetById(id);
            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy dữ liệu" });

            return Json(new { success = true, data = item });
        }
    }
}
