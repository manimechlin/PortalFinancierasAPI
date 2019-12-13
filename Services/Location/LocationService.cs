using BusinessEntities.Location;
using DataAccess.Interfaces;
using Services.Helper;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Location
{
    public class LocationService: ILocationService
    {
        private ILocationRepository _ILocationRepository;

        public LocationService(ILocationRepository _LocationRepository)
        {
            _ILocationRepository = _LocationRepository;
        }

        private DataSet result = null;
        private MixData _mixData = null;

        public LocationModel GetLocation(string userid, string cc_cve, int type, string value)
        {
            result = null;
            LocationModel response = null;
            _mixData = new MixData();
            try
            {
                result = _ILocationRepository.GetLocation(_mixData.D(userid), cc_cve, type, value);
                response = new LocationModel();

                if (result != null)
                {
                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        response.Latitud = row["Latitud"].ToString();
                        response.Longitud = row["Longitud"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                response = null;
            }

            return response;
        }
    }
}
