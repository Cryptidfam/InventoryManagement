using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace InventoryManagement.Management
{
    public class DataManager
    {
        private string filePath = "inventory.json"; // Stores inventory data

        public List<Item> LoadData()
        {
            if (!File.Exists(filePath)) return new List<Item>();
            var json = File.ReadAllText(filePath); // Read file
            // Converts the JSON string into a List<Item> object, if that fails, create new list
            return JsonConvert.DeserializeObject<List<Item>>(json) ?? new List<Item>();
        }

        public void SaveData(List<Item> items)
        {
            // Prevent an ambiguity error by including Newtonsoft.Json.Formatting explicitly:
            var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json); // Write the JSON string
        }
    }
}
