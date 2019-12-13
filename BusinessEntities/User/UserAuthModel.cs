using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserAuthModel
    {
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string dealerid { get; set; }
        [DataMember]
        public string profile { get; set; }
        [DataMember]
        public int profileid { get; set; }
    }
}
