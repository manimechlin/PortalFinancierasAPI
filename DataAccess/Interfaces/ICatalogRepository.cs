using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ICatalogRepository
    {
        DataSet GetConcessionary(string cc_tipo);
        DataSet GetBranchOffice(string cc_tipo, string cc_cve);
        DataSet GetCity();
        DataSet GetProfile(string groupname);
        DataSet GetVehicleDataCatalog(string key, string brand, string model, string version, string colour);
        DataSet GetDealerAddress(string cc_cve);
        DataSet GetLocationOptions(string groupname);
        DataSet GetCatologCommand();
    }
}
