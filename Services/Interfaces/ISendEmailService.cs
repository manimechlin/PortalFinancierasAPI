using BusinessEntities.Base;
using BusinessEntities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface ISendEmailService
    {
        ApiResponse mail(string file_xml);
        BranchOfficeModel GetBranchOffice(string cc_tipo, string cc_cve);
        void Send_mail(string file, int user, string sucursal, int records, int templateId, string mail);
        void GenerateExcelFile(System.Data.DataTable dataTable, string filename, int user, string sucursal);
    }
}
