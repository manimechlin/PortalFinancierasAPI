using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ILocationRepository
    {
        DataSet GetLocation(string userid, string cc_cve, int type, string value);
    }
}
