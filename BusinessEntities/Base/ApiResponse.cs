using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessEntities.Base
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string MsgError { get; set; }
        public object Data { get; set; }

        public ApiResponse()
        {
            this.Success = false;
            this.MsgError = null;
            this.Data = null;
        }

        public void SendSuccess(string result)
        {
            this.Success = true;
            this.MsgError = null;
            this.Data = result;
        }
    }
}
