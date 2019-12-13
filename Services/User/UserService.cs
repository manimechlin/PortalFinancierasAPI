using BusinessEntities.Base;
using BusinessEntities.User;
using DataAccess.Interfaces;
using Services.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _IUserRepository;

        public UserService(IUserRepository _UserRepository)
        {
            _IUserRepository = _UserRepository;
        }

        private DataSet result = null;
        private MixData _mixData = null;
        public List<UserModel> GetUsers(string cc_cve)
        {
            result = null;
            List<UserModel> response = null;
            try
            {
                response = new List<UserModel>();
                _mixData = new MixData();
                result = _IUserRepository.GetUsers(cc_cve);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new UserModel()
                        {
                            userid = _mixData.E(row["userid"].ToString()),
                            name = row["name"].ToString(),
                            profile = row["profile"].ToString(),
                            concessionaire = row["concessionaire"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return response;
        }
        public UserDetailModel GetUserDetail(string cc_cve, string id)
        {
            result = null;
            UserDetailModel response = null;
            try
            {
                _mixData = new MixData();
                response = new UserDetailModel();
                int userid = Convert.ToInt32(_mixData.D(id));

                result = _IUserRepository.GetUserDetail(cc_cve, userid);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.userid = row["userid"].ToString();
                        response.name = row["name"].ToString();
                        response.fec_nac = row["fec_nac"].ToString();
                        response.email = row["email"].ToString();
                        response.ruc = row["RUC"].ToString();
                        response.address = row["address"].ToString();
                        response.phonenumber = row["phonenumber"].ToString();
                        response.cd_cve = row["cd_cve"].ToString();
                        response.city = row["city"].ToString();
                        response.profileid = row["profileid"].ToString();
                        response.profile = row["profile"].ToString();
                        response.concessionaireid = row["concessionaireid"].ToString();
                        response.concessionaire = row["concessionaire"].ToString();
                        response.branch_cve = row["branch_cve"].ToString();
                        response.branchoffice = row["branchoffice"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return response;
        }

        public UserModel SaveUser(string cc_cve, string suc_cve, string name, string fec_nac, string email, string RIF, string address, string phonenumber, string cc_city, string password, int status, int profileid)
        {
            result = null;
            UserModel response = null;
            _mixData = new MixData();

            try
            {
                
                result = _IUserRepository.SaveUser(cc_cve, suc_cve, name, fec_nac, email, RIF, address, phonenumber, cc_city, _mixData.E(password), status, profileid);
                response = new UserModel();
                
                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.userid = row["userid"].ToString();
                        response.name = row["name"].ToString();
                        response.concessionaire = row["concessionaire"].ToString();
                        response.profile = row["profile"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }

        public UserAuthModel FindByEmailAsync(string email)
        {
            return _IUserRepository.FindByEmailAsync(email);
        }

        public bool RememberPassword(string userid, string email, string pass)
        {
            result = null;
            bool response = false;
            try
            {
                _mixData = new MixData();
                result = _IUserRepository.RememberPassword(userid, email, _mixData.D(pass));

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response = Convert.ToBoolean(row["IsSent"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return response;
        }

        public UserAsyncModel ValidateUser(string userid, string cc_cve)
        {
            result = null;
            UserAsyncModel response = null;

            try
            {
                result = _IUserRepository.ValidateUser(userid, cc_cve);
                response = new UserAsyncModel();

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.email = row["email"].ToString();
                        response.password = row["password"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }
            return response;
        }
    }
}
