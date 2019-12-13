using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Email
{
    public class EmailInformation
    {

        //rt_fin_getNotificationChannels
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string DealerId { get; set; }
        [DataMember]
        public int ChannelTypeId { get; set; }
        [DataMember]
        public int NotificationTypeId { get; set; }
        [DataMember]
        public string ChannelValue { get; set; }
        [DataMember]
        public byte IsEnabled { get; set; }
       
    }
}
