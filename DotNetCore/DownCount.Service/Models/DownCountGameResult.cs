using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownCount.Service.Models
{
    public class DownCountGameResult
    {
        public bool ExactResult { get; set; }
        public decimal ResultValue { get; set; }
        public string Result { get; set; }
    }
}
