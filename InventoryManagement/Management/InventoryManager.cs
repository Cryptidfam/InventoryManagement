using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class InventoryManager
    {
        private List<Item> items = new List<Item>(); // Stores a list of items

        // Returns the entire list of items currently in the inventory
        public List<Item> GetItems() => items;

        // Adds a new Item object to the inventory
        public void AddItem(Item item) => items.Add(item);

        public void RemoveItem(int id) // REMOVES THE ITEM BASED ON ID
        {
            // Uses LINQ (FirstOrDefault) to find the first item matching the given Id
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null) items.Remove(item);
        }

        // Retrieves a single item based on its Id
        public Item GetItemById(int id) => items.FirstOrDefault(i => i.Id == id);

        // Updates an item based on its id
        public void UpdateItem(int id, Item updatedItem)
        {
            var item = GetItemById(id);
            if (item != null)
            {
                item.Name = updatedItem.Name;
                item.Category = updatedItem.Category;
                item.Quantity = updatedItem.Quantity;
                item.Price = updatedItem.Price;
                //item.Description = updatedItem.Description;
            }
        }
    }

}
