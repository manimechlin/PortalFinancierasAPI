using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Configuration
{
    public class AppSettings
    {
        public int ApplicationId { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public string DescriptionSetting { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public class AppSettingsRequest
    {
        public int Id { get; set; }
    }
}
