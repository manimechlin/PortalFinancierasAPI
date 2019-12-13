using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Email;
using BusinessEntities.Installation;
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
    public class ActionsController : ControllerBase
    {
        private IinstallationService _installationService;
        ApiResponse response = new ApiResponse();
        public ActionsController(IinstallationService installationService)
        {
            _installationService = installationService;
        }

        [HttpGet]
        public IActionResult GetInstallation(string lpar1)
        {
			         
            string datos = HttpUtility.UrlDecode(lpar1);
            GetInstallation Info = new GetInstallation();
            List<GetInstallation> result_list = new List<GetInstallation>();

            string data = Helper.cryptoLib.EncryptDecryptfromJson(datos, Helper.EncryptionMode.Decrypt);                      
            GetInstallation userData = JsonConvert.DeserializeObject<GetInstallation>(data);
            if (userData.TypeId == 2)
            {
                MixData mixData = new MixData();
                var User = mixData.D(userData.Filtro);
                userData.Filtro = User;
            }
          
            
            try
            {
                response.Data = _installationService.GetInstallationRequest(userData.DealerId, userData.TypeId, userData.Filtro);

                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : Helper.cryptoLib.EncryptDecrypt(json, Helper.EncryptionMode.Encrypt);
                
                response.SendSuccess(encryptedString);
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }
       
        [HttpPost]
        public IActionResult Post([FromBody]RequestFormat r)
        {
			string result = Convert.ToString(r.lpar1);
			string datos = HttpUtility.UrlDecode(result);
            InstallationRequest Info = new InstallationRequest();
            List<InstallationRequest> result_list = new List<InstallationRequest>();
            string data = Helper.cryptoLib.EncryptDecryptfromJson(datos, Helper.EncryptionMode.Decrypt);
            List<InstallationRequest> userData = JsonConvert.DeserializeObject<List<InstallationRequest>>(data);
            MixData mixData = new MixData();
            if(userData.Count>0)
            {
                foreach(InstallationRequest request in userData)
                {
                    var user = mixData.D(request.CreatedBy);
                    request.CreatedBy = user;
                    result_list.Add(new InstallationRequest()
                    {Id=request.Id,
                    TypeId=request.TypeId,
                    DealerId=request.DealerId,
                    StatusId=request.StatusId,
                    VIN=request.VIN,
                    Plate=request.Plate,
                    VehicleModel=request.VehicleModel,
                    VehicleBrand=request.VehicleBrand,
                    VehicleVersion=request.VehicleVersion,
                    VehicleColor=request.VehicleColor,
                    VehicleYear=request.VehicleYear,
                    ContractMonthsTiempo=request.ContractMonthsTiempo,
                    InstallationDate=request.InstallationDate,
                    Address=request.Address,
                    City=request.City,
                    ContactName=request.ContactName,
                    Phone=request.Phone,
                    Comments=request.Comments,
                        CreatedBy = request.CreatedBy   
                    });

                }
            }
            
            userData[0].CreatedBy =Convert.ToString(User);
            //string jsonReport = JsonConvert.SerializeObject(userData, Newtonsoft.Json.Formatting.Indented);
            string jsonReport = JsonConvert.SerializeObject(result_list, Newtonsoft.Json.Formatting.Indented);
            try
            {
                //response.Data = _installationService.saveInstallationRequest(data);
                response.Data = _installationService.saveInstallationRequest(jsonReport);
                string json = JsonConvert.SerializeObject(response.Data);
                var encryptedString = string.IsNullOrEmpty(json) ? null : Helper.cryptoLib.EncryptDecrypt(json, Helper.EncryptionMode.Encrypt);
               
                response.SendSuccess(encryptedString);
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}