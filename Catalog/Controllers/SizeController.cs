using BaseBusiness.Model;
using Catalog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var sizes = (await _sizeService.GetAll()).AsQueryable();
            return Json(sizes);
        }


        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SizeModel size)
        {
            bool result = false;
            string message = "";

            try
            {
                if (size.Id == 0 || size.Id == null)
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

            return Json(new { success = result, message, data = size });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            bool result = await _sizeService.Delete(id);
            return Json(new { success = result, message = result ? "Xóa thành công" : "Xóa thất bại", id });
        }
    }
}
