using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BusinessEntities.Email
{
   public class NotificationChannelModel
    {

        //rt_fin_saveNotificationChannel
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
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
