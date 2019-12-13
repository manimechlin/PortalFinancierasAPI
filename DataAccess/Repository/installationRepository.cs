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
    public class installationRepository : IinstallationRepository, IDisposable
    {
        private IDBHelper _dbHelper;
        public installationRepository(IDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
        }
        public virtual DataSet GetInstallationRequest(string DealerId, int TypeId,string Filtro)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@DealerId", DealerId));
                parameters.Add(new SqlParameter("@TypeId", TypeId));
                parameters.Add(new SqlParameter("@Filtro", Filtro));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getInstallationRequestList", parameters);
            }
            catch (Exception ex)
            {
                
            }
            return ds;
        }
        public virtual DataSet saveInstallationRequest(string Xmlinstallation)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Xmlinstallation", Xmlinstallation));
                
                ds = _dbHelper.ExecuteProcedure("rt_fin_saveInstallationRequest", parameters);
            }
            catch(Exception ex)
            {

            }
            return ds;
        }
        public void Dispose()
        {

        }
    }
}
