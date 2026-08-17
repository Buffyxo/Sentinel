using System;
using System.Collections.Generic;
using System.Text;

namespace Sentinel.Models
{
    public class Alert
    {
        public string DeviceName { get; set; } = "";

        public string Message { get; set; } = "";

        public string Severity { get; set; } = "";

        public DateTime Timestamp { get; set; }
    }
}
