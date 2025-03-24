using System;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Data;

namespace InventoryManagement.ItemManagement
{
    public class ItemDataManager : GenericDataManager<Item>
    {
        public ItemDataManager() : base("ItemData.json") { } // Call the base constructor with the file name
        protected override bool FilterItems(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return true; // Check if the search query is empty
            if (obj is Item item) // return objects which match the search query
            {
                return item.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) || 
                       item.Category.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
        public static ItemDataManager LoadData() // GenericDataManager's LoadData method is not used here, don't feel the need to use it.
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "ItemData.json";
            if (!File.Exists(filePath))
            {
                return new ItemDataManager();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings() // Settings for deserializing and serializing the JSON data.
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented
            };

            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<ItemDataManager>(json, settings);

            data.ItemsView = CollectionViewSource.GetDefaultView(data.Items);
            data.ItemsView.Filter = data.FilterItems;
            data.ReattachEventHandlers();
            return data;
        }
    }
}
