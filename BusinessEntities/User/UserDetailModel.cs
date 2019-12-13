using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserDetailModel
    {
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string fec_nac { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string ruc { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string phonenumber { get; set; }
        [DataMember]
        public string cd_cve { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string profileid { get; set; }
        [DataMember]
        public string profile { get; set; }
        [DataMember]
        public string concessionaireid { get; set; }
        [DataMember]
        public string concessionaire { get; set; }
        [DataMember]
        public string branch_cve { get; set; }
        [DataMember]
        public string branchoffice { get; set; }
    }
}
