using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Repository
{
    public class ConfigurationDbRepository : IConfigurationDbRepository
    {
        private IDBHelper _dbHelper;
        public ConfigurationDbRepository(IDBHelper _DBHelper)
        {
            _dbHelper = _DBHelper;
        }

        public DataSet GetApplicationSettings(int id)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ApplicationId", id));
                ds = _dbHelper.ExecuteProcedure("rt_cc_get_ApplicationSettings", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }
    }
}
