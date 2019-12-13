using BusinessEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
  public interface IEmailService
    {
        ApiResponse GetEmail();
        ApiResponse SaveInformation(int Id, string UpdatedBy, string DealerId, int ChannelTypeId, int NotificationTypeId, string ChannelValue, byte IsEnabled);
    }
}
