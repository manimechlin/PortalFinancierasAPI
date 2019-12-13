using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
   public interface IinstallationRepository
    {
        DataSet GetInstallationRequest(string DealerId, int TypeId, string Filtro);
        DataSet saveInstallationRequest(string Xmlinstallation);
    }
}
