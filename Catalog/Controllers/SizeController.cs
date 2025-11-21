using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseBusiness.Model;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Modules.Catalog.Services;

namespace Catalog.Controllers
{
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

        [HttpGet("get-all")]
        public IActionResult GetList()
        {
            var sizes = _sizeService.GetAll();
            return Json(new { success = true, data = sizes });
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] SizeModel size)
        {
            bool result = false;
            string message = "";
            try
            {
                if (size.Id == 0)
                {
                    result = _sizeService.Create(size);
                    message = result ? "Tạo kích thước thành công." : "Tạo kích thước thất bại.";
                }
                else
                {
                    result = _sizeService.Update(size);
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
        public IActionResult Delete(long id)
        {
            var result = _sizeService.Delete(id);
            return Json(new
            {
                success = result,
                message = result ? "Xóa thành công." : "Xóa thất bại"
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
            var item = _sizeService.GetById(id);
            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy dữ liệu" });

            return Json(new { success = true, data = item });
        }
    }
}
