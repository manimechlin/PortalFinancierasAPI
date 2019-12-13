using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalFinancierasAPI.Controllers.Resources
{
    public class AccessTokenResource
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }
        public string value { get; set; }
    }
}
