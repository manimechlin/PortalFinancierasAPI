using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Massive
{
   public class MassiveInformation
    {
        [DataMember]
        public string license_Plate { get; set; }
        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public string observations { get; set; }
    }
}
