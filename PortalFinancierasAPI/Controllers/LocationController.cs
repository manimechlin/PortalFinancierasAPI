using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : Controller
    {
        private ILocationService _ILocationService;
        private ApiResponse response;

        public LocationController(ILocationService _LocationService)
        {
            _ILocationService = _LocationService;
        }

        [HttpGet]
        public IActionResult GetLocation(string lpar1)
        {
            string result = Convert.ToString(lpar1);
            string datos = HttpUtility.UrlDecode(result);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            LocationInfoModel _LocationInfoModel = JsonConvert.DeserializeObject<LocationInfoModel>(data);

            string encryptedString = string.Empty;
            string json = string.Empty;
            response = new ApiResponse();

            try
            {

                LocationModel LocationInfo = _ILocationService.GetLocation(_LocationInfoModel.userid, _LocationInfoModel.cc_cve, _LocationInfoModel.type, _LocationInfoModel.value);
                if (LocationInfo != null)
                {
                    response.Data = LocationInfo;
                    response.Success = true;
                }

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