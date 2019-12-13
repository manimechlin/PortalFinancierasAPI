using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Base;
using BusinessEntities.Email;
using DataAccess.Interfaces;
using DataAccess.Repository;
using IRT.CustomLog;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services.Email
{
   public class EmailService :  IEmailService
    {
        private IEmailRepository _IEmailRepository;
        private readonly LogService _LogService = new LogService();
        NotificationChannelModel ChannelModel = new NotificationChannelModel();
        ApiResponse response = new ApiResponse();
        DataSet result = null;
        string connectionLog = null;
        string _AplicationId = null;
        private IConfiguration _configuration;
        public EmailService(IEmailRepository emailRepository, IConfiguration Configuration)
        {
            _IEmailRepository = emailRepository;
            _configuration = Configuration;
            connectionLog = _configuration.GetSection("appSettings").GetSection("ConnectionLog").Value;
            _AplicationId = _configuration.GetSection("appSettings").GetSection("AplicationID").Value;
        }

        
        public ApiResponse GetEmail()
        {
           
            List<EmailInformation> ListEmail  = new List<EmailInformation>();
            try
            {
              
                result = _IEmailRepository.GetEmail();
                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        ListEmail.Add(new EmailInformation()
                        {
                            Id = Convert.ToInt16(row["Id"]),
                            DealerId = Convert.ToString(row["DealerId"]),
                            ChannelTypeId = Convert.ToInt16(row["ChannelTypeId"]),
                            NotificationTypeId = Convert.ToInt16(row["NotificationTypeId"]),
                            ChannelValue = Convert.ToString(row["ChannelValue"]),
                            IsEnabled = Convert.ToByte(row["IsEnabled"])

                        });
                    }
                }
                response.Success = true;
                response.Data = ListEmail;
            }
            catch (Exception ex)
            {
                response.Success = false;
                //  registrar en el DB-Log
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "EmailService.GetEmail", $"Parameters: empty", "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;

            }
            return response; 
        }

        public ApiResponse SaveInformation(int Id, string UpdatedBy, string DealerId, int ChannelTypeId, int NotificationTypeId, string ChannelValue, byte IsEnabled)
        {
            NotificationChannelModel channelModel = new NotificationChannelModel();
            try
            {
               
                result = _IEmailRepository.SaveNotification(Id, UpdatedBy, DealerId, ChannelTypeId, NotificationTypeId, ChannelValue, IsEnabled);
                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        channelModel.Id = Convert.ToInt32(row["Id"]);
                       // id = ChannelModel;
                        
                        
                    }
                    response.Success = true;
                    //JsonConvert.SerializeObject(response);
                    response.Data = channelModel; 
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                string parameters = "Parameters: Id: {0}, UpdatedBy: {1}, DealerId: {2}, ChannelTypeId: {3}, NotificationTypeId: {4}, ChannelValue: {5}, IsEnabled: {6}";
                long errorID = _LogService.WriteToLog(Convert.ToInt32(_AplicationId), "EmailService.SaveInformation", string.Format(parameters, Id, UpdatedBy, DealerId, ChannelTypeId, NotificationTypeId, ChannelValue, IsEnabled), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", connectionLog);
                response.MsgError = "Ocurrrio un error y se registro con el id:" + errorID;
            }
            return response;
        }
    }
}
