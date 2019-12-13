using BusinessEntities.Base;
using BusinessEntities.Report;
using BusinessEntities.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _ReportService;
        public ReportController(IReportService reportService)
        {
            _ReportService = reportService;
        }
        [HttpGet]


        public IActionResult Get(string lpar1)
        {

            string data2 = cryptoLib.EncryptDecryptfromJson(HttpUtility.UrlDecode(lpar1), EncryptionMode.Decrypt);
            ReportInformation userData = JsonConvert.DeserializeObject<ReportInformation>(data2);
            try
            {
                ApiResponse response = new ApiResponse();
                response.Data = _ReportService.GetReport(userData.StartDate, userData.EndDate, userData.command, userData.serial, userData.CreatedBy);
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
