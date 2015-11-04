using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace EsriDeviceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: EsriDeviceSimulator.exe <IotHubConnectionString>");
                return;
            }

            string iotHubConnectionString = args[0];

            List<GeoDeviceSimulator> sims = new List<GeoDeviceSimulator>();

            IDictionary<string, IList<GeoData>> deviceDataMap = GeoDataLoader.GetGeoDataCollection("Demo2GF.csv");
            
            // make sure devices are registered
            EnsureDevicesAsync(iotHubConnectionString, deviceDataMap.Keys.ToList()).Wait();

            foreach(string deviceId in deviceDataMap.Keys)
            {
                sims.Add(new GeoDeviceSimulator(iotHubConnectionString, deviceId, deviceDataMap[deviceId], TimeSpan.FromSeconds(1)));
            }

            foreach(var sim in sims)
            {
                sim.StartAsync();
            }

            Console.ReadLine();
            foreach (var sim in sims)
            {
                sim.StartAsync();
            }

            Console.ReadLine();
        }

        static async Task EnsureDevicesAsync(string connectionString, IList<string> deviceIds)
        {
            RegistryManager rm = RegistryManager.CreateFromConnectionString(connectionString);
            foreach(string deviceId in deviceIds)
            {
                Console.WriteLine("Verifying device: " + deviceId);
                Device d = await rm.GetDeviceAsync(deviceId);
                if(d == null)
                {
                    Console.WriteLine("Adding non-existent device: " + deviceId);
                    await rm.AddDeviceAsync(new Device(deviceId));
                }
            }
        }
    }
}
