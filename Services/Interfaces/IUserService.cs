using BusinessEntities.Base;
using BusinessEntities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetUsers(string cc_cve);
        UserDetailModel GetUserDetail(string cc_cve, string id);
        UserModel SaveUser(string cc_cve, string suc_cve, string name, string fec_nac, string email, string RIF, string address, string phonenumber, string cc_city, string password, int status, int profileid);
        UserAuthModel FindByEmailAsync(string email);
        bool RememberPassword(string userid, string email, string pass);
        UserAsyncModel ValidateUser(string userid, string cc_cve);
    }
}
