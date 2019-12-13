using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigurationDbController : ControllerBase
    {
        private IConfigurationDbService _IConfigurationDbService;

        private ApiResponse response;

        public ConfigurationDbController(IConfigurationDbService _configurationDbService)
        {
            _IConfigurationDbService = _configurationDbService;
        }

        [HttpGet]
        public IActionResult GetUsers(string lpar1)
        {
            string result = Convert.ToString(lpar1);
            string datos = HttpUtility.UrlDecode(result);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            AppSettingsRequest appSettingsRequest = JsonConvert.DeserializeObject<AppSettingsRequest>(data);

            string encryptedString = string.Empty;
            string json = string.Empty;
            response = new ApiResponse();

            try
            {
                List<AppSettings> appSettings = _IConfigurationDbService.GetApplicationSettings(appSettingsRequest.Id);
                response.Data = appSettings;
                response.Success = true;

                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }

            return Ok(encryptedString);
        }
    }
}