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
    [Route("color")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetList()
        {
            var colors = (await _colorService.GetAll()).AsQueryable();
            return Json(colors);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] ColorModel color)
        {
            bool result = false;
            string message = "";
            try
            {
                if (color.Id == 0 || color.Id == null)
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
            return Json(new { success = result, message, data = color });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool result = await _colorService.Delete(id);
            return Json(new { success = result, message = result ? "Xóa màu sắc thành công." : "Xóa thất bại", id });
        }
    }
}
