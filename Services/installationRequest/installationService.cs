using BusinessEntities.Base;
using BusinessEntities.Catalog;
using BusinessEntities.Installation;
using DataAccess.Interfaces;
using IRT.CustomLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Services.installationRequest
{
   public class installationService : IinstallationService
    {
        private IinstallationRepository _IinstallationRepository;
        private ISendEmailService _SendEmailService;
        private readonly LogService _LogService = new LogService();
        string connectionLog = null;
        string _AplicationId = null;
        private IConfiguration _configuration;
        ApiResponse response = new ApiResponse();
        DataSet result = null;

        public installationService(IinstallationRepository installationRepository, ISendEmailService SendEmailService, IConfiguration Configuration)
        {
            _IinstallationRepository = installationRepository;
            _configuration = Configuration;
            _SendEmailService = SendEmailService;
            connectionLog = _configuration.GetSection("appSettings").GetSection("ConnectionLog").Value;
            _AplicationId = _configuration.GetSection("appSettings").GetSection("AplicationID").Value;
        }
        public ApiResponse GetInstallationRequest(string DealerId, int TypeId, string Filtro)
        {
            List<InstallationRequest> Request = new List<InstallationRequest>();
            try
            {
                result = _IinstallationRepository.GetInstallationRequest(DealerId, TypeId, Filtro);
                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        Request.Add(new InstallationRequest()
                        {
                            Id= Convert.ToInt32(row["Id"]),
                            TypeId = Convert.ToInt32(row["TypeId"]),
                            DealerId = Convert.ToString(row["DealerId"]),
                            StatusId = Convert.ToInt32(row["StatusId"]),
                            VIN = Convert.ToString(row["VIN"]),
                            Plate = Convert.ToString(row["Plate"]),
                            VehicleModel = Convert.ToString(row["VehicleModel"]),
                            VehicleBrand = Convert.ToString(row["VehicleBrand"]),
                            VehicleVersion = Convert.ToString(row["VehicleVersion"]),
                            VehicleColor = Convert.ToString(row["VehicleColor"]),
                            VehicleYear = Convert.ToString(row["VehicleYear"]),
                            ContractMonthsTiempo = Convert.ToInt32(row["ContractMonthsTiempo"]),
                            InstallationDate = Convert.ToDateTime(row["InstallationDate"]),
                            Address = Convert.ToString(row["Address"]),
                            City = Convert.ToString(row["City"]),
                            ContactName = Convert.ToString(row["ContactName"]),
                            Phone = Convert.ToString(row["Phone"]),
                            Comments = Convert.ToString(row["Comments"]),
                            CreatedBy = Convert.ToString(row["CreatedBy"])
                           
                        });
                    }
                 }
                response.Success = true;
                response.Data = Request;
            }
            catch(Exception ex)
            {
                response.Success = false;
                string parameters = "Parameters: DealerId: {0}, CreatedBy: {1}, VIN: {2}";
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "installationService.GetInstallationRequest", string.Format(parameters, DealerId, TypeId, Filtro), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;

            }
            return response;
        }
        public ApiResponse saveInstallationRequest(string Xmlinstallation)
        {
            XmlDocument doc = new XmlDocument();
            string sucursal_ = null;
            try
            {
                InstallationRequest installation = new InstallationRequest();
                List<InstallationRequest> Installation_lst = new List<InstallationRequest>();
                List<InstallationRequest> ListRequest= new List<InstallationRequest>();
                List<InstallationRequest> ListUnistall = new List<InstallationRequest>();
                using (var reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(Xmlinstallation), XmlDictionaryReaderQuotas.Max))
                {
                    XElement xml = XElement.Load(reader);
                    doc.LoadXml(xml.ToString());
                }

                result = _IinstallationRepository.saveInstallationRequest(doc.InnerXml);
                string jsonReport = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                var contentJo = (JObject)JsonConvert.DeserializeObject(jsonReport);
                JObject organizationsJArray = (JObject)(contentJo.SelectToken("Table") as JArray).First();
                var JArray = contentJo["Table"].Value<JArray>();              
                string e_mail = organizationsJArray.Value<string>("ContactName");
                var lstInfo = JArray.ToObject<List<InstallationRequest>>();
                int registros = JArray.Count;

                Installation_lst = lstInfo;
                if (registros > 0)
                {
                    foreach (InstallationRequest request in Installation_lst)
                    {
                        installation.TypeId = request.TypeId;
                       
                        if (installation.TypeId==1)
                        {
                            ListRequest.Add(new InstallationRequest()
                            {
                                StatusId = installation.StatusId
                            });

                        }
                        if (installation.TypeId == 2)
                        {
                            ListUnistall.Add(new InstallationRequest()
                            {
                                StatusId = installation.StatusId
                            });
                        }
                        

                        
                    }
                    if (ListRequest.Count > 0)
                    {


                        int count_Installation = ListRequest.Count;
                        _SendEmailService.Send_mail(null, 0, null, count_Installation, 2, e_mail);
                    }
                    if (ListUnistall.Count > 0)
                    {
                        int count_unistallation = ListUnistall.Count;
                        _SendEmailService.Send_mail(null, 0, null, count_unistallation, 3, e_mail);
                    }

                }

                response.Success = true;
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Success = false;
                string parameters = "Parameters: Xmlinstallation: {0}";
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "installationService.saveInstallationRequest", string.Format(parameters, doc.InnerXml), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;
            }
            return response;

        }        
    }
}
