using BusinessEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IReportService
    {
        ApiResponse GetReport(string StartDate, string EndDate, string command, string serial, string CreatedBy);
    }
}
