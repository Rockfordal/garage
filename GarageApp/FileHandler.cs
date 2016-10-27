using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GarageApp
{
    class FileHandler
    {
        static string url = @"c:\Users\Kurt\garage.json";

        public static void SaveAllGarages(IEnumerable<Garage<Vehicle>> data)
        {
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(url, json);
        }

        public static int LoadAllGarages(GarageHandler gh)
        {
            try
            {
                string jsonData = File.ReadAllText(url);
                gh.garages = JsonConvert.DeserializeObject<List<Garage<Vehicle>>>(jsonData);
            }
            catch (FileNotFoundException)
            {
                //SaveAllGarages(gh.garages);
            }
            return gh.garages.Count;
        }

    }
}
