using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InventoryManagement.Management
{
    public class DataManager
    {
        private string filePath = AppDomain.CurrentDomain.BaseDirectory + "inventory.json"; // Stores inventory data
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public static DataManager LoadData()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "inventory.json"; // Stores inventory data
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            if (!File.Exists(filePath)) return new DataManager();
            var json = File.ReadAllText(filePath); // Read file
            // Converts the JSON string into a List<Item> object, if that fails, create new list
            return JsonConvert.DeserializeObject<DataManager>(json, serializerSettings);
        }

        public void SaveData()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                // Prevent an ambiguity error by including Newtonsoft.Json.Formatting explicitly:
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(this, serializerSettings);
            File.WriteAllText(filePath, json); // Write the JSON string
        }
    }
}
