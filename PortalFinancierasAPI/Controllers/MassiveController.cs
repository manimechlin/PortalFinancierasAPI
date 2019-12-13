using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using BusinessEntities.Base;
using BusinessEntities.Massive;
using BusinessEntities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalFinancierasAPI.Helper;
using Services.Helper;
using Services.Interfaces;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassiveController : ControllerBase
    {
        private ISendEmailService _SendEmailService;
        public MassiveController(ISendEmailService SendEmailService)
        {
            _SendEmailService = SendEmailService;

        }
        [HttpGet]
        public IActionResult Get(string lpar1)
        {
            string datos = HttpUtility.UrlDecode(lpar1);
            
            string data = Helper.cryptoLib.EncryptDecryptfromJson(datos, Helper.EncryptionMode.Decrypt);
            IdInformation userData = JsonConvert.DeserializeObject<IdInformation>(data);
            MixData mixData = new MixData();
           var User= mixData.D(userData.UserId);
            userData.UserId =User;
            string jsonReport = JsonConvert.SerializeObject(userData, Newtonsoft.Json.Formatting.Indented);
            
            try
            {
                ApiResponse response = new ApiResponse();
                response.Data = _SendEmailService.mail(jsonReport);
                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : Helper.cryptoLib.EncryptDecrypt(json, Helper.EncryptionMode.Encrypt);
                response.Data = encryptedString;
                return Ok(response.Data);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}