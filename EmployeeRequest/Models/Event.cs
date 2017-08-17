using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRequest.Models
{
    class Event
    {
        public string id { get; set; }
        public string eventType { get; set; }
        public string subject { get; set; }
        public DateTime eventTime { get; set; }
        public Employee data { get; set; }
    }
}
