using BusinessEntities.Base;
using DataAccess.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Report
{
   public class ReportService : IReportService
    {
        private IReportRepository _ReportRepository;
        ApiResponse response = new ApiResponse();
        public ReportService(IReportRepository ReportRepository)
        {
            _ReportRepository = ReportRepository;
        }
        public ApiResponse GetReport(string StartDate, string EndDate, string command, string serial, string CreatedBy)
        {
            string Start = String.Format("{0:d/M/yyyy HH:mm:ss}", StartDate);
            string End = String.Format("{0:d/M/yyyy HH:mm:ss}", EndDate);
            try
            {
                var result = _ReportRepository.GetReport(Start, End, command, serial, CreatedBy);
                string jsonReport = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                var contentJo = (JObject)JsonConvert.DeserializeObject(jsonReport);
                response.Success = true;
              
                response.Data = contentJo;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
