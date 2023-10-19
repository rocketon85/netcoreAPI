using Microsoft.Extensions.Options;
using netcoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreAPI.Tests.Services
{
    public class BaseService
    {
        public BaseService()
        {
            var startUp = new StartUp();
        }
    }
}
