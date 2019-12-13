using BusinessEntities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IConfigurationDbService
    {
        List<AppSettings> GetApplicationSettings(int id);
    }
}
