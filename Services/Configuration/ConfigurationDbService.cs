using BusinessEntities.Configuration;
using DataAccess.Interfaces;
using Services.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configuration
{
    public class ConfigurationDbService : IConfigurationDbService
    {
        private IConfigurationDbRepository _IConfigurationDbRepository;

        public ConfigurationDbService(IConfigurationDbRepository _configurationDbRepository)
        {
            _IConfigurationDbRepository = _configurationDbRepository;
        }

        private DataSet result = null;
        private MixData _mixData = null;
        public List<AppSettings> GetApplicationSettings(int id)
        {
            result = null;
            List<AppSettings> response = new List<AppSettings>();
            try
            {
                _mixData = new MixData();
                result = _IConfigurationDbRepository.GetApplicationSettings(id);

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        var appSetting = new AppSettings()
                        {
                            ApplicationId = Convert.ToInt32(row["ApplicationId"]),
                            ParameterKey = row["ParameterKey"].ToString(),
                            ParameterValue = row["ParameterValue"].ToString(),
                            DescriptionSetting = row["DescriptionSetting"].ToString(),

                        };
                        if (row["CreatedOn"] != DBNull.Value)
                        {
                            appSetting.CreatedOn = Convert.ToDateTime(row["CreatedOn"]);
                        }
                        if (row["CreatedBy"] != DBNull.Value)
                        {
                            appSetting.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                        }
                        if (row["UpdatedOn"] != DBNull.Value)
                        {
                            appSetting.UpdatedOn = Convert.ToDateTime(row["UpdatedOn"]);
                        }
                        if (row["UpdatedBy"] != DBNull.Value)
                        {
                            appSetting.UpdatedBy = Convert.ToInt32(row["UpdatedBy"]);
                        }

                        response.Add(appSetting);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return response;
        }
    }
}
