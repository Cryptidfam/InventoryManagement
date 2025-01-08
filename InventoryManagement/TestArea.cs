using InventoryManagement.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    // I wanted to add a console to check if DataManager.cs works, but Idk how to add console here
    public class TestArea
    {
        public static void RunTests()
        {
            DataManager dataManager = new DataManager();

            // Test 1: Save some items
            var items = new List<Item>
        {
            new Item { Id = 1, Name = "Apple", Category = "Fruit", Quantity = 10, Price = 0.99m },
            new Item { Id = 2, Name = "Banana", Category = "Fruit", Quantity = 5, Price = 0.59m }
        };
            dataManager.SaveData(items);
            Console.WriteLine("Data saved!");

            // Test 2: Load the items
            var loadedItems = dataManager.LoadData();
            foreach (var item in loadedItems)
            {
                Console.WriteLine($"Loaded Item: {item.Name}, Quantity: {item.Quantity}");
            }
        }
    }

}
