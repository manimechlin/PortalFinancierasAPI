using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Installation
{
  public class GetInstallation
    {
        [DataMember]
        public string DealerId { get; set; }
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public string Filtro { get; set; }
    }
}
