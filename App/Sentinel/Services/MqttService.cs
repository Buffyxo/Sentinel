using MQTTnet;
using Sentinel.Models;
using Serilog;
using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Diagnostics;

namespace Sentinel.Services
{
    public class MqttService
    {
        private readonly IMqttClient _client;

        public event Action<EnvironmentReading>? EnvironmentUpdated;

        public MqttService()
        {
            var factory = new MqttClientFactory();

            _client = factory.CreateMqttClient();

            _client.ApplicationMessageReceivedAsync += OnMessageReceived;
        }

        public async Task ConnectAsync()
        {
            try
            {
                Debug.WriteLine("=== MQTT: Connecting to broker ===");

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("localhost", 1883)
                    .WithClientId("Sentinel")
                    .Build();

                await _client.ConnectAsync(options);

                Debug.WriteLine("=== MQTT: Connected ===");

                var topicFilter = new MqttTopicFilterBuilder()
                    .WithTopic("sentinel/environment")
                    .Build();

                await _client.SubscribeAsync(topicFilter);

                Debug.WriteLine("=== MQTT: Subscribed to sentinel/environment ===");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== MQTT ERROR: {ex} ===");
            }
        }

        private Task OnMessageReceived(
    MqttApplicationMessageReceivedEventArgs e)
        {
            string json = Encoding.UTF8.GetString(
                e.ApplicationMessage.Payload.ToArray());

            Debug.WriteLine("=== MQTT MESSAGE RECEIVED ===");
            Debug.WriteLine($"Topic: {e.ApplicationMessage.Topic}");
            Debug.WriteLine($"Payload: {json}");

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var reading =
                    JsonSerializer.Deserialize<EnvironmentReading>(json, options);

                if (reading != null)
                {
                    Console.WriteLine("=== DESERIALIZED ENVIRONMENT ===");
                    Debug.WriteLine(
                        $"Temperature: {reading.Temperature} °C");

                    Debug.WriteLine(
                        $"Humidity: {reading.Humidity}%");

                    EnvironmentUpdated?.Invoke(reading);
                }
            }
            catch (JsonException ex)
            {
                Debug.WriteLine("=== JSON DESERIALIZATION ERROR ===");
                Debug.WriteLine($"Payload was: {json}");
                Debug.WriteLine(ex.ToString());
            }

            return Task.CompletedTask;
        }
    }
}