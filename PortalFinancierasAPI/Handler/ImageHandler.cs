using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalFinancierasAPI.Handler
{

    public interface IImageHandler
    {
        Task<ActionResult> UploadImage(IFormFile logofile,IFormFile backfile, string logoname, string backname);
    }
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }
        public async Task<ActionResult> UploadImage(IFormFile logofile,IFormFile backfile,string logoname, string backname)
        {
            var result = await _imageWriter.UploadImage(logofile,backfile,logoname,backname);
            return new ObjectResult(result);
        }
    }
}
