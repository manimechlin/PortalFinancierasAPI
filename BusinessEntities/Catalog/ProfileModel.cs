﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Catalog
{
    public class ProfileModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
