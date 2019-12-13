using BusinessEntities.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILocationService
    {
        LocationModel GetLocation(string userid, string cc_cve, int type, string value);
    }
}
