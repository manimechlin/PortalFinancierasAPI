using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.User
{
    public class UserMainModel
    {
        [DataMember]
        public string userid { get; set; }
        [DataMember]
        public string cc_cve { get; set; }
    }
}
