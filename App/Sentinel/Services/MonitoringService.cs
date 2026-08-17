using System;
using Sentinel.Models;

namespace Sentinel.Services
{
    public class MonitoringService
    {
        public List<Device> GetDevices()
        {
            return new List<Device>

            {
                new Device

                {
                    Name = "Router-01",
                    Type = "Router",
                    Status = "Online",
                    IpAddress = "192.168.1.1",
                    LastSeen = DateTime.Now

                },

                new Device

                {
                    Name = "Server-01",
                    Type = "Server",
                    Status = "Offline",
                    IpAddress = "192.168.1.10",
                    LastSeen = DateTime.Now
                },

                new Device

                {
                    Name = "IoT-Sensor-01",
                    Type = "IoT Sensor",
                    Status = "Offline",
                    IpAddress = "192.168.1.50",
                    LastSeen = DateTime.Now.AddMinutes(-15)
                },

                new Device
                {
                    Name = "Switch-01",
                    Type = "Network Switch",
                    Status = "Online",
                    IpAddress = "192.168.1.2",
                    LastSeen = DateTime.Now
                }

            };

        }

        public List<Alert> GetAlerts(List<Device> devices)
        {
            var alerts = new List<Alert>();

            foreach (var device in devices)
            {
                if (device.Status.Equals("Offline", StringComparison.OrdinalIgnoreCase))
                {
                    alerts.Add(new Alert
                    {
                        DeviceName = device.Name,
                        Message = "Device is currently offline",
                        Severity = "Warning",
                        Timestamp = device.LastSeen
                    });
                }
            }

            return alerts;
        }

        public EnvironmentReading GetEnvironmentReading()
        {
            return new EnvironmentReading
            {
                Temperature = 27.8,
                Humidity = 72
            };
        }
    }
}
