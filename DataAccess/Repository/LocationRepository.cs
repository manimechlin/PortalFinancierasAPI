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
    public class LocationRepository : ILocationRepository
    {
        private IDBHelper _dbHelper;
        public LocationRepository(IDBHelper _DBHelper)
        {
            _dbHelper = _DBHelper;
        }
        public DataSet GetLocation(string userid, string cc_cve, int type, string value)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@userid", userid));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                parameters.Add(new SqlParameter("@type", type));
                parameters.Add(new SqlParameter("@value", value));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getCoordinates", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }
    }
}
