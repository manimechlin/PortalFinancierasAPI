using BusinessEntities.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        DataSet GetUsers(string cc_cve);
        DataSet GetUserDetail(string cc_cve, int userid);
        DataSet SaveUser(string cc_cve, string suc_cve, string name, string fec_nac, string email, string RIF, string address, string phonenumber, string cc_city, string password, int status, int profileid);
        UserAuthModel FindByEmailAsync(string email);
        DataSet RememberPassword(string userid, string email, string password);
        DataSet ValidateUser(string userid, string cc_cve);
    }
}
