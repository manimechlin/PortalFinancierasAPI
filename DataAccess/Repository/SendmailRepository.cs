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
   public class SendmailRepository: ISendmailRepository, IDisposable
    {
        private IDBHelper _dbHelper;
        public SendmailRepository(IDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
        }

        public void Dispose()
        {

        }
        public virtual DataSet Filexml(string file_xml)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@file_xml", file_xml));
                ds = _dbHelper.ExecuteProcedure("rtt_getMassiveReport", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;

        }
        public DataSet GetBranchOffice(string cc_tipo, string cc_cve)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_tipo", cc_tipo));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                ds = _dbHelper.ExecuteProcedure("qcomsuc1", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }
    }
}
