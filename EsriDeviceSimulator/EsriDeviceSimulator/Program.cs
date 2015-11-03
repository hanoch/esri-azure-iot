using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace EsriDeviceSimulator
{
    class Program
    {
        // Pass the iothub connection string as argument to this exe
        static string IotHubConnectionString = "<ConnectionString>";

        // NOTE: all of these devices must already be registered in the iot hub
        static string[] DeviceIdList = { 
                                       "testdevice1", 
                                       "testdevice2", 
                                       "testdevice3" 
                                   };

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: EsriDeviceSimulator.exe <IotHubConnectionString>");
                return;
            }

            IotHubConnectionString = args[0];

            List<GeoDeviceSimulator> sims = new List<GeoDeviceSimulator>();

            foreach(string deviceId in DeviceIdList)
            {
                sims.Add(new GeoDeviceSimulator(IotHubConnectionString, deviceId, TimeSpan.FromSeconds(3)));
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
    }
}
