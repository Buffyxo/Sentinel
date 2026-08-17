using System;

namespace Sentinel.Models
{
    public class Device
    {
        public string Name { get; set; } = "";

        public string Type { get; set; } = "";

        public string Status { get; set; } = "";

        public string IpAddress { get; set; } = "";

        public DateTime LastSeen { get; set; }

    }
}
