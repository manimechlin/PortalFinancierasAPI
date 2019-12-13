using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserDataModel
    {
        public string cc_cve { get; set; }
        public string suc_cve { get; set; }
        public string name { get; set; }
        public string fec_nac { get; set; }
        public string email { get; set; }
        public string RIF { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string cc_city { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public int profileid { get; set; }
    }
}
