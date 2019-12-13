using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Catalog
{
    public class BranchOfficeModel
    {
        [DataMember]
        public string suc_cve { get; set; }
        [DataMember]
        public string name { get; set; }
    }
}
