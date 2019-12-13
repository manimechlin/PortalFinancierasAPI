using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DBHelper : IDBHelper
    {
        #region Declare Variables 

        SqlConnection connection = null;
        SqlCommand command = null;
        // public string connectionstring = "Database=skytex;Server=10.21.1.142\\RTCORTBISNSN, 62293;User ID = APP_NSNRTBIS; password=TdksJ9T#3?3n;Pooling=false";

        public IConfiguration _configuration;
        private string connectionstring;
       
        #endregion

        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = _configuration.GetSection("connectionStrings").GetSection("skytex").Value;
           
        }
        public DataSet ExecuteProcedure(string commandName, List<SqlParameter> paramCollection)
        {
            DataSet ds = new DataSet();
            try
            {
                connection = new SqlConnection(connectionstring);
                command = new SqlCommand(commandName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (SqlParameter item in paramCollection)
                {
                    command.Parameters.Add(item);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public int ExecuteNonQuery(string commandName, List<SqlParameter> paramCollection)
        {
            int rowAffected = 0;
            try
            {
                connection = new SqlConnection(connectionstring);
                connection.Open();
                command = new SqlCommand(commandName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (SqlParameter item in paramCollection)
                {
                    command.Parameters.Add(item);
                }
                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return rowAffected;
        }

    }
}
