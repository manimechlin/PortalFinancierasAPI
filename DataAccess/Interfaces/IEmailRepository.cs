using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
  public interface IEmailRepository
    {
        DataSet GetEmail();
        DataSet SaveNotification(int Id, string UpdatedBy, string DealerId, int ChannelTypeId, int NotificationTypeId, string ChannelValue, byte IsEnabled);

    }
}
