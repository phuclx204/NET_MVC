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

        [HttpGet("get-all")]
        public IActionResult GetList()
        {
            var colors = _colorService.GetAll();
            return Json(new { success = true, data = colors });
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] ColorModel color)
        {
            bool result = false;
            string message = "";
            try
            {
                if (color.Id == 0)
                {
                    result = _colorService.Create(color);
                    message = result ? "Tạo màu sắc thành công." : "Tạo màu sắc thất bại.";
                }
                else
                {
                    result = _colorService.Update(color);
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
        public IActionResult Delete( long id)
        {
            var result = _colorService.Delete(id);
            return Json(new {
                success = result, 
                message = result ? "Xóa màu sắc thành công." : "Xóa thất bại" 
            });
        }

        // 1. Action trả về View màn hình Save (Dùng cho cả Thêm mới và Sửa)
        [HttpGet("form")]
        public IActionResult Form(long id = 0)
        {
            ViewBag.Id = id;
            return View("Save");
        }

        [HttpGet("detail/{id}")]
        public IActionResult GetDetail(long id)
        {
            var item = _colorService.GetById(id);
            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy dữ liệu" });

            return Json(new { success = true, data = item });
        }
    }
}
