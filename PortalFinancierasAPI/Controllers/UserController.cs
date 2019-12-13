using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Request;
using BusinessEntities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;

namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _IUserService;
        private ApiResponse response;
        public UserController (IUserService _userservice)
        {
            _IUserService = _userservice;
        }

        [HttpGet]
        public IActionResult GetUsers(string lpar1)
        {
            string result = Convert.ToString(lpar1);
            string datos = HttpUtility.UrlDecode(result);
            
            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            UserInfoModel userinfo = JsonConvert.DeserializeObject<UserInfoModel>(data);

            string encryptedString = string.Empty;
            string json = string.Empty;
            response = new ApiResponse();

            try
            {
                
                switch (userinfo.type)
                {
                    case 1:
                        List<UserModel> Modeluser = _IUserService.GetUsers(userinfo.cc_cve);
                        if (Modeluser.Count > 0)
                        {
                            response.Data = _IUserService.GetUsers(userinfo.cc_cve);
                            response.Success = true;
                        }
                        break;
                    case 2:
                        UserDetailModel datadetailuser = _IUserService.GetUserDetail(userinfo.cc_cve, userinfo.userid);
                        if (datadetailuser != null)
                        {
                            response.Data = _IUserService.GetUserDetail(userinfo.cc_cve, userinfo.userid);
                            response.Success = true;
                        }
                        break;
                }

                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
		
			return Ok(encryptedString);
		}

        [HttpPost]
		public IActionResult SaveUser([FromBody]RequestFormat r)
		{
			string result = Convert.ToString(r.lpar1);
			string datos = HttpUtility.UrlDecode(result);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            UserDataModel catalogmodel = JsonConvert.DeserializeObject<UserDataModel>(data);
            string encryptedString = string.Empty;

            try
            {
                response = new ApiResponse();


                UserModel _Usermodel = _IUserService.SaveUser(catalogmodel.cc_cve, catalogmodel.suc_cve, catalogmodel.name, catalogmodel.fec_nac, catalogmodel.email, catalogmodel.RIF, catalogmodel.address, catalogmodel.phonenumber, catalogmodel.cc_city, catalogmodel.password, catalogmodel.status, catalogmodel.profileid);

                if (_Usermodel != null)
                {
                    response.Data = _Usermodel;
                    response.Success = true;
                }

                string json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            return Ok(encryptedString);
        }

        [HttpPut]
        public IActionResult RememberPassword([FromBody]RequestFormat r)
		{
			string result = Convert.ToString(r.lpar1);
            string datos = HttpUtility.UrlDecode(result);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            UserMainModel usermain = JsonConvert.DeserializeObject<UserMainModel>(data);

            string encryptedString = string.Empty;
            string json = string.Empty;
            response = new ApiResponse();

            try
            {
                UserAsyncModel UserAsync_ = _IUserService.ValidateUser(usermain.userid, usermain.cc_cve);
                if (UserAsync_ != null)
                {
                    bool emailIsSent = _IUserService.RememberPassword(usermain.userid, UserAsync_.email, UserAsync_.password);
                    response.Data = emailIsSent;
                    response.Success = true;
                }

                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            catch (Exception ex)
            {
                json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            return Ok(encryptedString);
        }
    }
}