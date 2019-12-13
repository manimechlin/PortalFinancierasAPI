using BusinessEntities.User;
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
    public class UserRepository : IUserRepository
    {
        private IDBHelper _dbHelper;
        public UserRepository(IDBHelper _DBHelper)
        {
            _dbHelper = _DBHelper;
        }
        
        DataSet IUserRepository.GetUsers(string cc_cve)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getUsers", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }

        DataSet IUserRepository.GetUserDetail(string cc_cve, int userid)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                parameters.Add(new SqlParameter("@userid", userid));
                ds = _dbHelper.ExecuteProcedure("rt_fin_getUserDetail", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }

        public DataSet SaveUser(string cc_cve, string suc_cve, string name, string fec_nac, string email, string RIF, string address, string phonenumber, string cc_city, string password, int status, int profileid)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                parameters.Add(new SqlParameter("@suc_cve", suc_cve));
                parameters.Add(new SqlParameter("@name", name));
                parameters.Add(new SqlParameter("@fec_nac", fec_nac));
                parameters.Add(new SqlParameter("@email", email));
                parameters.Add(new SqlParameter("@RIF", RIF));
                parameters.Add(new SqlParameter("@address", address));
                parameters.Add(new SqlParameter("@phonenumber", phonenumber));
                parameters.Add(new SqlParameter("@cc_city", cc_city));
                parameters.Add(new SqlParameter("@Password", password));
                parameters.Add(new SqlParameter("@status", status));
                parameters.Add(new SqlParameter("@perfilid", profileid));
                ds = _dbHelper.ExecuteProcedure("rt_fin_saveUser", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }

        public UserAuthModel FindByEmailAsync(string email)
        {
            UserAuthModel user = new UserAuthModel();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email", email));
            DataSet ds = _dbHelper.ExecuteProcedure("rt_fin_getUserByEmail", parameters);

            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    user.userid = row["userid"].ToString();
                    user.profile = row["profile"].ToString();
                    user.profileid = int.Parse(row["profileid"].ToString());
                    user.dealerid = row["concessionaireid"].ToString();
                    user.email = row["email"].ToString();
                    user.password = row["password"].ToString();
                    break;
                }
            }

            return user;
        }

        public DataSet RememberPassword(string userid, string email, string password)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@userid", email));
                parameters.Add(new SqlParameter("@email", email));
                parameters.Add(new SqlParameter("@Password", password));
                ds = _dbHelper.ExecuteProcedure("rt_fin_RememberPassword", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }
        public DataSet ValidateUser(string userid, string cc_cve)
        {
            DataSet ds = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@userid", userid));
                parameters.Add(new SqlParameter("@cc_cve", cc_cve));
                ds = _dbHelper.ExecuteProcedure("rt_fin_validateUser", parameters);
            }
            catch (Exception ex)
            {
                // LibLogging.WriteErrorToDB("AccountRepository", "GetDDD", ex);
            }
            return ds;
        }
    }
}
