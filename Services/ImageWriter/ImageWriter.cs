using Microsoft.AspNetCore.Http;
using Services.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ImageWriter
{
    public class ImageWriter : IImageWriter
    {
        public async Task<string> UploadImage(IFormFile logofile, IFormFile backfile, string logoname, string backname)
        {
            if (CheckImageFile(logofile) || CheckImageFile(backfile))
            {
                return await WriteFile(logofile, backfile, logoname, backname);
            }
            return "Invalid Image File";
        }


        private bool CheckImageFile(IFormFile file)
        {
            if (file != null)
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> WriteFile(IFormFile logofile, IFormFile backfile, string logoname, string backname)
        {
            try
            {
                if (logofile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", logoname);
                    if (File.Exists(path)) { File.SetAttributes(path, FileAttributes.Normal);  File.Delete(path); }
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await logofile.CopyToAsync(bits);
                        File.SetAttributes(path, FileAttributes.Normal);
                    }
                }
                if (backfile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", backname);
                    if (File.Exists(path)) { File.SetAttributes(path, FileAttributes.Normal); File.Delete(path); }
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await backfile.CopyToAsync(bits); File.SetAttributes(path, FileAttributes.Normal);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }
    }
}
