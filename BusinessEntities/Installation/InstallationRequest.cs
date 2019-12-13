using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Installation
{
    public class InstallationRequest
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int TypeId { get; set; }
        [DataMember]
        public string DealerId { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public string VIN { get; set; }
        [DataMember]
        public string Plate { get; set; }
        [DataMember]
        public string VehicleModel { get; set; }
        [DataMember]        
        public string VehicleBrand { get; set; }
        [DataMember]
        public string VehicleVersion { get; set; }
        [DataMember]
        public string VehicleColor { get; set; }
        [DataMember]
        public string VehicleYear { get; set; }
        [DataMember]
        public int ContractMonthsTiempo { get; set; }
        [DataMember]
        public DateTime InstallationDate { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ContactName { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string CreatedOn { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime LastUpdateOn { get; set;}
        [DataMember]
        public int LastUpdateBy { get; set;}
        [DataMember]
        public string Status { get; set; }
    }
}
