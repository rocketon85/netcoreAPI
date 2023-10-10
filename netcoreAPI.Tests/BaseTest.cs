using Microsoft.Extensions.Options;
using netcoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreAPI.Tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            StartUp.SetUp();
        }
    }
}
