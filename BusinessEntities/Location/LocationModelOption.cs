using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Location
{
    public class LocationModelOption
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}
