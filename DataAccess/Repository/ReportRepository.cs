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
  public class ReportRepository : IReportRepository, IDisposable
    {
        private IDBHelper _dbHelper;
        public ReportRepository(IDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
        }
        public void Dispose()
        {

        }
        public virtual DataSet GetReport(string StartDate, string EndDate, string command, string serial, string CreatedBy)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@StartDate", StartDate));
                parameters.Add(new SqlParameter("@EndDate", EndDate));
                parameters.Add(new SqlParameter("@command", command));
                parameters.Add(new SqlParameter("@serial", serial));
                parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
                ds = _dbHelper.ExecuteProcedure("GetCommandReport", parameters);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }
    }
}
