using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class References
    {
        private IConfiguration _configuration;

        protected int applicationId; 
        public References(IConfiguration Configuration)
        {
            _configuration = Configuration;

            applicationId = int.Parse(_configuration.GetSection("connectionStrings").GetSection("AplicationID").Value);
        }
    }
}
