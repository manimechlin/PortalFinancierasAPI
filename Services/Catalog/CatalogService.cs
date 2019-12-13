using BusinessEntities.Base;
using BusinessEntities.Catalog;
using BusinessEntities.Location;
using DataAccess.Interfaces;
using IRT.CustomLog;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private readonly LogService _LogService = new LogService();
        private ICatalogRepository _ICatalogRepository;
        private IConfiguration config;
        private string _ApplicationId;
        private string _connectionstring;
        public CatalogService(ICatalogRepository CatalogRepository, IConfiguration configuration)
        {
            _ICatalogRepository = CatalogRepository;
            config = configuration;
            _ApplicationId = config.GetSection("appSettings").GetSection("AplicationID").Value;
            _connectionstring = config.GetSection("connectionStrings").GetSection("skytex").Value;
        }

        private DataSet result = null;

        public List<BranchOfficeModel> GetBranchOffice(string cc_tipo, string cc_cve)
        {
            result = null;
            List<BranchOfficeModel> response = null;
            try
            {

                response = new List<BranchOfficeModel>();
                result = _ICatalogRepository.GetBranchOffice(cc_tipo, cc_cve);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new BranchOfficeModel()
                        {
                            suc_cve = row["suc_cve"].ToString(),
                            name = row["name"].ToString()
                        });
                    }

                }
                
            }
            catch(Exception ex)
            {
                string parameters = "Parameters: cc_tipo: {0}, cc_cve: {1}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetBranchOffice", string.Format(parameters, cc_tipo, cc_cve), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public List<CityModel> GetCity()
        {
            result = null;
            List<CityModel> response = null;
            try
            {
                response = new List<CityModel>();
                result = _ICatalogRepository.GetCity();

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new CityModel()
                        {
                            Name = row["cd_nom"].ToString(),
                            cd_cve = row["cd_cve"].ToString()
                        });
                    }
                }

            }
            catch(Exception ex)
            {
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetCity", $"Parameters: empty", "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public List<ConcessionaryModel> GetConcessionary(string cc_tipo)
        {
            result = null;
            List<ConcessionaryModel> response = null;
            try
            {
                response = new List<ConcessionaryModel>();
                result = _ICatalogRepository.GetConcessionary(cc_tipo);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new ConcessionaryModel()
                        {
                            Name = row["nom2"].ToString(),
                            cc_cve = row["cc_cve"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = "Parameters: cc_tipo: {0}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetConcessionary", string.Format(parameters, cc_tipo), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public List<ProfileModel> GetProfile(string groupname)
        {
            result = null;
            List<ProfileModel> response = null;
            try
            {
                response = new List<ProfileModel>();
                result = _ICatalogRepository.GetProfile(groupname);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new ProfileModel()
                        {
                            Description = row["ItemDescription"].ToString(),
                            Id = row["ItemValue"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = "Parameters: groupname: {0}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetProfile", string.Format(parameters, groupname), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public List<VehicleDataCatalogModel> GetVehicleDataCatalog(string key, string brand, string model, string version, string colour)
        {
            result = null;
            List<VehicleDataCatalogModel> response = null;
            try
            {
                response = new List<VehicleDataCatalogModel>();
                result = _ICatalogRepository.GetVehicleDataCatalog(key, brand, model, version, colour);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new VehicleDataCatalogModel()
                        {
                            id = row["inf_cve"].ToString(),
                            description = row["inf_nom"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = "Parameters: key: {0}, brand: {1}, model: {2}, version: {3}, colour: {4}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetVehicleDataCatalog", string.Format(parameters, key, brand, model, version, colour), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public string GetDealerAddress(string cc_cve)
        {
            result = null;
            string response = string.Empty;

            try
            {
                result = _ICatalogRepository.GetDealerAddress(cc_cve);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response = row["address"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                string parameters = "Parameters: cc_cve: {0}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetVehicleDataCatalog", string.Format(parameters, cc_cve), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }

        public List<LocationModelOption> GetLocationOptions(string groupname)
        {
            result = null;
            List<LocationModelOption> response = null;
            try
            {
                response = new List<LocationModelOption>();
                result = _ICatalogRepository.GetLocationOptions(groupname);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new LocationModelOption()
                        {
                            description = row["ItemDescription"].ToString(),
                            type = row["ItemValue"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = "Parameters: groupname: {0}";
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetLocationOptions", string.Format(parameters, groupname), "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }
        public List<CatalogCommands> GetCatalogCommands()
        {
            result = null;
            List<CatalogCommands> response = null;
            try
            {
                response = new List<CatalogCommands>();
                result = _ICatalogRepository.GetCatologCommand();
                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Add(new CatalogCommands()
                        {
                            Command = row["Command"].ToString(),
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _LogService.WriteToLog(Convert.ToInt32(_ApplicationId), "CatalogService.GetCatalogCommands", $"Parameters: empty", "ERROR", $"Error: {ex.Message} :: StackTrace: {ex.StackTrace}", _connectionstring);
            }
            return response;
        }
    }
}
