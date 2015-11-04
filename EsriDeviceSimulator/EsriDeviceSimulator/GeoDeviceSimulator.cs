using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace EsriDeviceSimulator
{
    public class GeoDeviceSimulator
    {
        readonly string deviceId;
        readonly string connectionString;
        readonly CancellationTokenSource cts;
        readonly DeviceClient deviceClient;
        readonly TimeSpan telemetryInterval;

        public GeoDeviceSimulator(string connectionString, string deviceId, TimeSpan telemetryInterval)
        {
            this.deviceId = deviceId;
            this.connectionString = connectionString;
            this.cts = new CancellationTokenSource();
            this.deviceClient = DeviceClient.CreateFromConnectionString(this.connectionString, deviceId);
            this.telemetryInterval = telemetryInterval;
        }

        public void StartAsync()
        {
            this.Log("Starting");
            Task.Run(() => this.ProcessNotificationsAsync());
            Task.Run(() => this.SendTelemetryAsync());
        }

        public void StopAsync()
        {
            this.Log("Stopping");
            this.cts.Cancel();
        }

        public async Task SendTelemetryAsync()
        {
            int sendCount = 0;
            GeoData[] geoDataList = GeoData.GetData("Demo2GF.csv").ToArray();
            int count = geoDataList.Length;
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    byte[] messageData = GenerateTelemetryBytes(this.deviceId, geoDataList[sendCount % count]);
                    using (Message m = new Message(messageData))
                    {
                        await this.deviceClient.SendEventAsync(m);
                        sendCount++;

                        if (sendCount % 10 == 0)
                        {
                            this.Log("Sent " + sendCount + " messages");
                        }

                        await Task.Delay(this.telemetryInterval);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            this.Log("Stopping telemetry pump");
        }

        public async Task ProcessNotificationsAsync()
        {
            while (!cts.IsCancellationRequested)
            {
                using (Message m = await this.deviceClient.ReceiveAsync(TimeSpan.FromSeconds(5)))
                {
                    if (m == null)
                    {
                        continue;
                    }

                    this.Log("== Notification recieved: " + System.Text.Encoding.UTF8.GetString(m.GetBytes()));
                    await this.deviceClient.CompleteAsync(m);
                }
            }
        }

        void Log(string message)
        {
            Console.WriteLine("[{0}] Device {1}: {2}", DateTime.Now, this.deviceId, message);
        }

        public static byte[] GenerateTelemetryBytes(string id, GeoData geoData)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                deviceId = id,
                geoData = geoData
            }));
        }

        static long GetEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return (long)t.TotalMilliseconds;
        }
    }
}
