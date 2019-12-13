using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Command;
using BusinessEntities.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private ICommandService _ICommandService;
        private ApiResponse response;
        public CommandController(ICommandService _CommandService)
        {
            _ICommandService = _CommandService;
        }

        [HttpPost]
        public IActionResult SendCommand([FromBody]RequestFormat r)
        {
            
            string datos = HttpUtility.UrlDecode(r.lpar1);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            VehicleCommandModel vehiclemodel = JsonConvert.DeserializeObject<VehicleCommandModel>(data);
            try
            {
                response = new ApiResponse();


                response.Data = _ICommandService.ValidateData("", vehiclemodel.serialesxml, vehiclemodel.usercve, vehiclemodel.password, vehiclemodel.command, 0, vehiclemodel.cccve, vehiclemodel.suc_cve, false, false);
                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
                response.SendSuccess(encryptedString);
                if (response != null)
                {
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
            }
            return Ok(response);
        }
    }
}