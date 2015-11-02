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
        // plug-in the iothub connection string here
        static string IotHubConnectionString = "HostName=esri-simulator-test.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0Hknvn6FdzXP4KUQm1H7TlaWfC8LtBunEDHXrYqBEME=";

        // NOTE: all of these devices must already be registered in the iot hub
        static string[] DeviceIdList = { 
                                       "testdevice1", 
                                       "testdevice2", 
                                       "testdevice3" 
                                   };

        static void Main(string[] args)
        {
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
