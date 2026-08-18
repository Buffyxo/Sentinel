using Sentinel.Models;
using Sentinel.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sentinel.ViewModels
{
    public class MainViewModel
    {
        private readonly MonitoringService _monitoringService;

        public List<Device> Devices { get; set; }

        public List<Alert> Alerts { get; set; }

        public EnvironmentReading Environment { get; set; }

        public int TotalDevices { get; set; }

        public int OnlineDevices { get; set; }

        public int AlertCount { get; set; }

        public MainViewModel()
        {
            _monitoringService = new MonitoringService();

            Devices = _monitoringService.GetDevices();

            Alerts = _monitoringService.GetAlerts(Devices);

            Environment = _monitoringService.GetEnvironmentReading();

            TotalDevices = Devices.Count;

            OnlineDevices =
                Devices.Count(d => d.Status == "Online");

            AlertCount = Alerts.Count;
        }
    }
}