using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserInfoModel
    {
        [DataMember]
        public int type { get; set; }
        [DataMember]
        public string cc_cve { get; set; }
        [DataMember]
        public string userid { get; set; }
    }
}
