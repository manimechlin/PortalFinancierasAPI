using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile logofile,IFormFile backfile,string logoname,string backname);
    }
}
