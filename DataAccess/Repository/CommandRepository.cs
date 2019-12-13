using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private IDBHelper _dbHelper;

        public DataSet ValidateData(string ef_cve, string serialesxml, string user_cve = null, string password = null, string command = null, int onlycontract = 0, string cc_cve = null, string project_cve = null, bool searchcontractitmov = false, bool searchpolvta = false)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ef_cve", ef_cve));
                parameters.Add(new SqlParameter("@SerialesXml", serialesxml));
                parameters.Add(new SqlParameter("@User_cve", user_cve));
                parameters.Add(new SqlParameter("@Password", password));
                parameters.Add(new SqlParameter("@comando", command));
                parameters.Add(new SqlParameter("@onlyContract", onlycontract));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                parameters.Add(new SqlParameter("@conf_cve", project_cve));
                parameters.Add(new SqlParameter("@searchContractItmov", searchcontractitmov));
                parameters.Add(new SqlParameter("@searchPolVta", searchpolvta));
                ds = _dbHelper.ExecuteProcedure("rt_pf_VehicleToValidateMassive_Generic_New", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }
        public DataSet SendCommand(string dispositivo_cve, string esptec_cve, string obs, string idultact, int dias, int politica, string serialesxml, int type, string command, string cc_cve, string suc_cve, string user_cve, string cctipo, string password)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@dispositivo_cve", dispositivo_cve));
                parameters.Add(new SqlParameter("@esptec_cve", esptec_cve));
                parameters.Add(new SqlParameter("@obs", obs));
                parameters.Add(new SqlParameter("@id_ulact", idultact));
                parameters.Add(new SqlParameter("@dias", dias));
                parameters.Add(new SqlParameter("@politica", politica));
                parameters.Add(new SqlParameter("@serialesxml", serialesxml));
                parameters.Add(new SqlParameter("@tipo", type));
                parameters.Add(new SqlParameter("@comando", command));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                parameters.Add(new SqlParameter("@GLN", suc_cve));
                parameters.Add(new SqlParameter("@user_cve", user_cve));
                parameters.Add(new SqlParameter("@cc_tipo", cctipo));
                parameters.Add(new SqlParameter("@password", password));
                ds = _dbHelper.ExecuteProcedure("rt_pf_insertNewLoteGeneric", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }
    }
}
