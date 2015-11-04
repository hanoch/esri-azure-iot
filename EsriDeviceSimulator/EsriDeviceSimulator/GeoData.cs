using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsriDeviceSimulator
{
    public class GeoData
    {
        public string Suspect { get; set; }
        public string TrackDate { get; set; }
        public string Sensor { get; set; }
        public string BatteryLevel { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DistanceFt { get; set; }
        public string DurationMin { get; set; }
        public string SpeedMph { get; set; }
        public string CourseDegree { get; set; }

        public static IEnumerable<GeoData> GetData(string fileName)
        {
            List<string> lines = File.ReadAllLines(fileName).ToList();

            lines.RemoveAt(0);

            foreach (string line in lines)
            {
                yield return Parse(line);
            }
        }

        static GeoData Parse(string data)
        {
            string[] tokens = data.Split(',');

            return new GeoData()
            {
                Suspect = tokens[0],
                TrackDate = tokens[1],
                Sensor = tokens[2],
                BatteryLevel = tokens[3],
                Latitude = tokens[4],
                Longitude = tokens[5],
                DistanceFt = tokens[6],
                DurationMin = tokens[7],
                SpeedMph = tokens[8],
                CourseDegree = tokens[9]
            };
        }
    }
}
