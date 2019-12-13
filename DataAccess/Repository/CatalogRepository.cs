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
    public class CatalogRepository : ICatalogRepository
    {
        private IDBHelper _dbHelper;
        public CatalogRepository(IDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
        }
        public DataSet GetBranchOffice(string cc_tipo, string cc_cve)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_tipo", cc_tipo));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getBranchOffice", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetCity()
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                ds = _dbHelper.ExecuteProcedure("qcomcd1", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetConcessionary(string cc_tipo)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@prm1", cc_tipo));
                ds = _dbHelper.ExecuteProcedure("qcomccve21", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetProfile(string groupname)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@GroupName", groupname));
                ds = _dbHelper.ExecuteProcedure("rt_cc_getCatalogList", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetVehicleDataCatalog(string key, string brand, string model, string version, string colour)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@key", key));
                parameters.Add(new SqlParameter("@marca", brand));
                parameters.Add(new SqlParameter("@modelo", model));
                parameters.Add(new SqlParameter("@version", version));
                parameters.Add(new SqlParameter("@color", colour));
                ds = _dbHelper.ExecuteProcedure("rt_cc_GetVehicleDataCatalogs", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetDealerAddress(string cc_cve)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getDealerAddress", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetLocationOptions(string groupname)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@GroupName", groupname));
                ds = _dbHelper.ExecuteProcedure("rt_cc_getCatalogList", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }

        public DataSet GetCatologCommand()
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                ds = _dbHelper.ExecuteProcedure("rtt_GetCatalogCommands", parameters);
            }
            catch (Exception ex)
            {
                //LibLogging.WriteErrorToDB("AccountRepository", "GetConfiguration", ex);
            }
            return ds;
        }
    }
}
