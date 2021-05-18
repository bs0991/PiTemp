using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiTempWebApp.Models
{
    public class PiTempData
    {
        public string Temperature_F { get; set; }
        public string Temperature_C { get; set; }
        public string Humidity { get; set; }
        public string LogDate { get; set; }
    }
}
