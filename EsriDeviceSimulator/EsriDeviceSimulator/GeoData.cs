using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsriDeviceSimulator
{
    public class GeoDataLoader
    {
        public static IDictionary<string, IList<GeoData>> GetGeoDataCollection(string filename)
        {
            Dictionary<string, IList<GeoData>> geoMap = new Dictionary<string, IList<GeoData>>();

            List<string> lines = File.ReadAllLines(filename).ToList();

            lines.RemoveAt(0);

            foreach(string line in lines)
            {
                GeoData newGeoData = GeoData.Parse(line);
                IList<GeoData> geoDataList = null;
                if(!geoMap.TryGetValue(newGeoData.Suspect, out geoDataList))
                {
                    geoDataList = new List<GeoData>();
                    geoMap.Add(newGeoData.Suspect, geoDataList);
                }
                geoDataList.Add(newGeoData);
            }

            return geoMap;
        }
    }

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

        private GeoData()
        {
        }

        static public GeoData Parse(string data)
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
