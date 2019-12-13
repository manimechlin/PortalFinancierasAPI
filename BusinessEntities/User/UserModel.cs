using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserModel
    {
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string profile { get; set; }
        [DataMember]
        public string concessionaire { get; set; }
    }
}
