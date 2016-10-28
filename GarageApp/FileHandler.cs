using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GarageApp
{
    class FileHandler
    {
        static string fileName = "garage.json";

        public static string getPath(string fileName)
        {
            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;

            if ( Environment.OSVersion.Version.Major >= 6 ) {
                path = Directory.GetParent(path).ToString();
            }
            return path + "\\" +  fileName;
        }

        public static void SaveAllGarages(IEnumerable<Garage<Vehicle>> data)
        {
            string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(getPath(fileName), json);
        }

        public static int LoadAllGarages(GarageHandler gh)
        {
            try
            {
                string jsonData = File.ReadAllText(getPath(fileName));
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
