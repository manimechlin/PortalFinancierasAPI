using BusinessEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IinstallationService
    {
        ApiResponse GetInstallationRequest(String DealerId, int TypeId, string Filtro);
        ApiResponse saveInstallationRequest(string Xmlinstallation);
    }
}
