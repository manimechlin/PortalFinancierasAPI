using BusinessEntities.Base;
using BusinessEntities.Catalog;
using BusinessEntities.Location;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICatalogService
    {
        List<ConcessionaryModel> GetConcessionary(string cc_tipo);
        List<BranchOfficeModel> GetBranchOffice(string cc_tipo, string cc_cve);
        List<CityModel> GetCity();
        List<ProfileModel> GetProfile(string groupname);
        List<VehicleDataCatalogModel> GetVehicleDataCatalog(string key, string brand, string model, string version, string colour);
        string GetDealerAddress(string cc_cve);
        List<LocationModelOption> GetLocationOptions(string groupname);
        List<CatalogCommands> GetCatalogCommands();
    }
}
