using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Massive
{
    public class IdInformation
    {
        [DataMember]
        public int DealerId { get; set; }
        [DataMember]
        public string UserId { get; set; }
       public List<commands> commands { get; set; }

    }
}
