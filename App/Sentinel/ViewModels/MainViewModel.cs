using Sentinel.Models;
using Sentinel.Services;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;

using System.Runtime.CompilerServices;

namespace Sentinel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MonitoringService _monitoringService;

        private readonly MqttService _mqttService;

        private EnvironmentReading _environment;

        public List<Device> Devices { get; set; }

        public List<Alert> Alerts { get; set; }

        public int TotalDevices { get; set; }

        public int OnlineDevices { get; set; }

        public int AlertCount { get; set; }

        public MainViewModel()
        {
            _monitoringService = new MonitoringService();

            _mqttService = new MqttService();

            Log.Information("MQTT service created.");

            _mqttService.EnvironmentUpdated += OnEnvironmentUpdated;

            Log.Information("Starting MQTT connection...");

            _ = ConnectMqttAsync();

            Devices = _monitoringService.GetDevices();

            Alerts = _monitoringService.GetAlerts(Devices);

            Environment = _monitoringService.GetEnvironmentReading();

            TotalDevices = Devices.Count;

            OnlineDevices =
                Devices.Count(d => d.Status == "Online");

            AlertCount = Alerts.Count;

            Log.Information("Devices loaded: {Count}", Devices.Count);
 

        }

        private void OnEnvironmentUpdated(EnvironmentReading reading)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Environment = reading;

                Log.Information(
                    "Environment updated: Temperature={Temperature}°C, Humidity={Humidity}%",
                    reading.Temperature,
                    reading.Humidity);
            });
        }

        private async Task ConnectMqttAsync()
        {
            await _mqttService.ConnectAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public EnvironmentReading Environment

        {

            get => _environment;

            set

            {

                _environment = value;

                OnPropertyChanged();

            }

        }
    }
}