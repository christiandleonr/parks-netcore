using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks.Services.Configuration
{
    public class ParksDatabaseSettings : IParksDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IParksDatabaseSettings
    {
        string ConnectionString {get; set;}

        string DatabaseName {get; set;}
    }
}