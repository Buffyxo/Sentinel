using Sentinel.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;


namespace Sentinel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MonitoringService _monitoringService;
        public MainWindow()
        {
            InitializeComponent();

            _monitoringService = new MonitoringService();

            var devices = _monitoringService.GetDevices();
            DeviceList.ItemsSource = devices;

            var alerts = _monitoringService.GetAlerts(devices);
            AlertList.ItemsSource = alerts;

            var environment = _monitoringService.GetEnvironmentReading();
            TemperatureText.Text = $"{environment.Temperature:F1} °C";
            HumidityText.Text = $"{environment.Humidity:F0} %";

            int totalDevices = devices.Count;
            int onlineDevices = devices.Count(d =>
                d.Status.Equals("Online", StringComparison.OrdinalIgnoreCase));
            int offlineDevices = devices.Count(d =>
                d.Status.Equals("Offline", StringComparison.OrdinalIgnoreCase));

            TotalDevicesText.Text = totalDevices.ToString();
            OnlineDevicesText.Text = onlineDevices.ToString();
            AlertCountText.Text = alerts.Count.ToString();

        }
    }
}