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
   public class EmailRepository : IEmailRepository, IDisposable
    {
        private IDBHelper _dbHelper;
        public EmailRepository(IDBHelper dBHelper)
        {
            _dbHelper = dBHelper;
        }

        public void Dispose()
        {
            
        }

        public virtual DataSet GetEmail()
        {
            DataSet ds = null;
          
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                ds = _dbHelper.ExecuteProcedure("rt_fin_getNotificationChannels", parameters);
            }
            catch (Exception ex)
            {
               // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;

        }
        public virtual DataSet SaveNotification(int Id, string UpdatedBy, string DealerId, int ChannelTypeId, int NotificationTypeId, string ChannelValue, byte IsEnabled)
        {
            DataSet ds = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id", Id));
                parameters.Add(new SqlParameter("@UpdatedBy", UpdatedBy));
                parameters.Add(new SqlParameter("@DealerId", DealerId));
                parameters.Add(new SqlParameter("@ChannelTypeId", ChannelTypeId));
                parameters.Add(new SqlParameter("@NotificationTypeId", NotificationTypeId));
                parameters.Add(new SqlParameter("@ChannelValue", ChannelValue));
                parameters.Add(new SqlParameter("@IsEnabled", IsEnabled));

                ds = _dbHelper.ExecuteProcedure("rt_fin_saveNotificationChannel", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;

        }
    }
}
