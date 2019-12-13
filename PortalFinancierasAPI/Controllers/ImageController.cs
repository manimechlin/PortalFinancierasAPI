using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortalFinancierasAPI.Handler;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/img")]
    public class ImageController : Controller
    {
        private readonly IImageHandler _imageHandler;
        public ImageController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            IFormFile logofile= Request.Form.Files["logofile"];
            IFormFile backfile = Request.Form.Files["backfile"];
            string logoname = Request.Form["logoname"];
            string backname = Request.Form["backname"];
            var result= await _imageHandler.UploadImage(logofile,backfile,logoname,backname);
            return Ok(result);
        }
    }
}