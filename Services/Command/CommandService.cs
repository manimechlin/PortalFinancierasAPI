using BusinessEntities.Base;
using BusinessEntities.Command;
using DataAccess.Interfaces;
using IRT.CustomLog;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Command
{
    public class CommandService : ICommandService
    {
        private readonly LogService _LogService = new LogService();
        private ICommandRepository _ICommandRepository;
        private IConfiguration config;
        private string _ApplicationId;
        private string _connectionstring;
        public CommandService(ICommandRepository _CommandRepository, IConfiguration configuration)
        {
            _ICommandRepository = _CommandRepository;
            config = configuration;
            _ApplicationId = config.GetSection("appSettings").GetSection("AplicationID").Value;
            _connectionstring = config.GetSection("connectionStrings").GetSection("skytex").Value;
        }
        private DataSet result = null;
        private ApiResponse response = null;

        public ApiResponse ValidateData(string ef_cve, string serialesxml, string user_cve = null, string password = null, string command = null, int onlycontract = 0, string cc_cve = null, string project_cve = null, bool searchcontractitmov = false, bool searchpolvta = false)
        {
            result = null;
            response = new ApiResponse();
            try
            {
                List<DataVehicleStatusModel> datavehiclestatusmodel = new List<DataVehicleStatusModel>();
                result = _ICommandRepository.ValidateData(ef_cve, serialesxml, user_cve, password, command, onlycontract, cc_cve, project_cve, searchcontractitmov, searchpolvta);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        datavehiclestatusmodel.Add(new DataVehicleStatusModel()
                        {
                            platinum = row["serial_cve"].ToString(),
                            contract = row["contrato"].ToString(),
                            dispositivo_cve = row["dispositivo_cve"].ToString(),
                            deviceid = row["deviceId"].ToString(),
                            error = int.Parse(row["ERROR"].ToString())
                        });
                    }
                    response.Success = true;
                    response.Data = datavehiclestatusmodel;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                string parameters = "Parameters: ef_cve: {0}, serialesxml: {1}, user_cve: {2}, password: {3}, command: {4}, onlycontract: {5}, cc_cve: {6}, project_cve: {7}, searchcontractitmov: {8}, searchpolvta: {9}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CommandService.ValidateData", string.Format(parameters, ef_cve, serialesxml, user_cve, password, command, onlycontract, cc_cve, project_cve, searchcontractitmov, searchpolvta), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public ApiResponse SendCommand(string dispositivo_cve, string esptec_cve, string obs, string idultact, int dias, int politica, string serialesxml, int type, string command, string cc_cve, string suc_cve, string user_cve, string cctipo, string password)
        {
            result = null;
            response = new ApiResponse();
            try
            {
                result = _ICommandRepository.SendCommand(dispositivo_cve, esptec_cve, obs, idultact, dias, politica, serialesxml, type, command, cc_cve, suc_cve, user_cve, cctipo, password);

                if (result != null)
                {
                    int _result = -1;
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        _result = int.Parse(row["next"].ToString());
                    }
                    response.Success = _result == -1 ? false : true;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                string parameters = "Parameters: dispositivo_cve: {0}, esptec_cve: {1}, obs: {2}, idultact: {3}, dias: {4}, politica: {5}, serialesxml: {6}, type: {7}, command: {8}, cc_cve: {9}, suc_cve: {10}, user_cve: {11}, cctipo: {12}, password: {13}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CommandService.SendCommand", string.Format(parameters, dispositivo_cve, esptec_cve, obs, idultact, dias, politica, serialesxml, type, command, cc_cve, suc_cve, user_cve, cctipo, password), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }
    }
}
