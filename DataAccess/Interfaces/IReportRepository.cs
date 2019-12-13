using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
  public interface IReportRepository
    {
        DataSet GetReport(string StartDate, string EndDate, string command, string serial, string CreatedBy);
    }
}
