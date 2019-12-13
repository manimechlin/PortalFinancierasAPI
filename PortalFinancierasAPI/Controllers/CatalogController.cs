using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusinessEntities.Base;
using BusinessEntities.Catalog;
using BusinessEntities.Location;
using BusinessEntities.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortalFinancierasAPI.Helper;
using Services.Interfaces;


namespace PortalFinancierasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ICatalogService _ICatalogService;
        private ApiResponse response;
        public CatalogController(ICatalogService CatalogService)
        {
            _ICatalogService = CatalogService;
        }

        [HttpGet]
        public IActionResult GetCatalog(string lpar1)
        {

            string datos = HttpUtility.UrlDecode(lpar1);

            string data = cryptoLib.EncryptDecryptfromJson(datos, EncryptionMode.Decrypt);
            CatalogModel catalogmodel = JsonConvert.DeserializeObject<CatalogModel>(data);
            string encryptedString = string.Empty;

            try
            {
                response = new ApiResponse();

                switch (catalogmodel.type)
                {
                    case 1:
                        List<BranchOfficeModel> BranchOfficeModel_ = _ICatalogService.GetBranchOffice(catalogmodel.cc_type, catalogmodel.cc_cve);
                        if (BranchOfficeModel_.Count > 0)
                        {
                            response.Success = true;
                            response.Data = BranchOfficeModel_;
                        }
                        break;
                    case 2:
                        List<CityModel> ListModelCity = _ICatalogService.GetCity();
                        if (ListModelCity.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListModelCity;
                        }
                        break;
                    case 3:
                        List<ConcessionaryModel> ListModelConcessionary = _ICatalogService.GetConcessionary(catalogmodel.cc_type);
                        if (ListModelConcessionary.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListModelConcessionary;
                        }
                        break;
                    case 4:
                        List<ProfileModel> ListModelProfile = _ICatalogService.GetProfile(catalogmodel.groupname);
                        if (ListModelProfile.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListModelProfile;
                        }
                        break;
                    case 5:
                        string[] vehicledata = catalogmodel.vehicledatacatalog.Split(",");
                        List<VehicleDataCatalogModel> ListModelvehicle = _ICatalogService.GetVehicleDataCatalog(vehicledata[0], vehicledata[1], vehicledata[2], vehicledata[3], vehicledata[4]);
                        if (ListModelvehicle.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListModelvehicle;
                        }
                        break;
                    case 6:
                        string address = _ICatalogService.GetDealerAddress(catalogmodel.cc_cve);
                        if (!String.IsNullOrEmpty(address))
                        {
                            response.Success = true;
                            response.Data = address;
                        }
                        break;
                    case 7:
                        List<LocationModelOption> ListModelLocation = _ICatalogService.GetLocationOptions(catalogmodel.groupname);
                        if (ListModelLocation.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListModelLocation;
                        }
                        break;
                    case 8:
                        List<CatalogCommands> ListCatalogCommands = _ICatalogService.GetCatalogCommands();
                        if (ListCatalogCommands.Count > 0)
                        {
                            response.Success = true;
                            response.Data = ListCatalogCommands;
                        }
                        break;

                }

                string json = JsonConvert.SerializeObject(response);
                encryptedString = string.IsNullOrEmpty(json) ? null : cryptoLib.EncryptDecrypt(json, EncryptionMode.Encrypt);
            }
            catch (Exception ex)
            {
                response.Success = false;
            }
            return Ok(encryptedString);

        }
    }
}