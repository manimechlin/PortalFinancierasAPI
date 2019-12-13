using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public interface IDBHelper
    {
        DataSet ExecuteProcedure(string commandName, List<SqlParameter> paramCollection);

        int ExecuteNonQuery(string commandName, List<SqlParameter> paramCollection);
    }
}
