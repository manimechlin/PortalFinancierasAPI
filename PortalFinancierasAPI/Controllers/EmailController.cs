using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Email;
using BusinessEntities.Email;
using Newtonsoft.Json;
using BusinessEntities.Base;
using PortalFinancierasAPI.Helper;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using BusinessEntities.Request;

namespace PortalFinancierasAPI.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController: ControllerBase
    {
        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        NotificationChannelModel ChannelModel = new NotificationChannelModel();
       
        
       
        [HttpGet]        
        public IActionResult  Get()
        {
            
            try
            {
                ApiResponse response = new ApiResponse();
                response.Data = _emailService.GetEmail();
                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
                response.Data = encryptedString;
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]

        [Route("SaveChannel")]
        public IActionResult SaveChannel([FromBody]RequestFormat r)
        {

			string datos = HttpUtility.UrlDecode(r.lpar1);
			string data2 = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);           
            NotificationChannelModel userData = JsonConvert.DeserializeObject<NotificationChannelModel>(data2);
            try
            {
                ApiResponse response = new ApiResponse();
                response.Data = _emailService.SaveInformation(userData.Id, userData.UpdatedBy, userData.DealerId, userData.ChannelTypeId, userData.NotificationTypeId, userData.ChannelValue, userData.IsEnabled);
                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt); 
                response.SendSuccess(encryptedString);
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
     }
}
