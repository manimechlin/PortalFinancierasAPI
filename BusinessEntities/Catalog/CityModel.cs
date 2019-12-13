using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Catalog
{
    public class CityModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string cd_cve { get; set; }
    }
}
