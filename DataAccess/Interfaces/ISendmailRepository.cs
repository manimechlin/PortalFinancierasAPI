using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
   public interface ISendmailRepository
    {
        DataSet Filexml(string file_xml);
        DataSet GetBranchOffice(string cc_tipo, string cc_cve);
    }
}
