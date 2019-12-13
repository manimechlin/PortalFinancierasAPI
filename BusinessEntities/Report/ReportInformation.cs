using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Report
{
   public class ReportInformation
    {
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }
        [DataMember]
        public string command { get; set; }
        [DataMember]
        public  string serial { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
    }
}
