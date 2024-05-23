using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreAPI.Contracts.Models.Responses
{
    public class ApiInfoResponse
    {
        public required string Author { get; set; }

        public required string Version { get; set; }
    }
}
