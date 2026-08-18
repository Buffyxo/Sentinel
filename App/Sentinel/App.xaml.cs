using Serilog;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace Sentinel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()

        {

            string logDirectory = Path.Combine(

                AppContext.BaseDirectory,

                "Logs");

            Directory.CreateDirectory(logDirectory);

            Log.Logger = new LoggerConfiguration()

                .MinimumLevel.Information()

                .WriteTo.File(

                    Path.Combine(logDirectory, "sentinel.log"),

                    rollingInterval: RollingInterval.Day)

                .CreateLogger();

            Log.Information("Sentinel application starting.");

        }
    }

}
