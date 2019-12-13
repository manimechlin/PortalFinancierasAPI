using BusinessEntities.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICommandService
    {
        ApiResponse ValidateData(string ef_cve, string serialesxml, string user_cve, string password, string command, int onlycontract, string cc_cve, string project_cve, bool searchcontractitmov, bool searchpolvta);
        ApiResponse SendCommand(string dispositivo_cve, string esptec_cve, string obs, string idultact, int dias, int politica, string serialesxml, int type, string command, string cc_cve, string suc_cve, string user_cve, string cctipo, string password);
    }
}
