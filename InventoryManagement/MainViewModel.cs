using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Management;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InventoryManagement
{
    // Acts as the bridge between the XAML UI and the logic.
    public class MainViewModel: INotifyPropertyChanged
    {        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private InventoryManager inventoryManager;
        private DataManager dataManager;

        public ObservableCollection<Item> Items { get; set; }

        private Item _selecteditem;
        public Item SelectedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public MainViewModel()
        {
            inventoryManager = new InventoryManager();
            dataManager = new DataManager();

            // Load data on startup
            var loadedItems = dataManager.LoadData();
            inventoryManager.GetItems().AddRange(loadedItems);
           // Items = new ObservableCollection<Item>(inventoryManager.GetItems());
           Items = new ObservableCollection<Item>();
        }

        // To do: Add a new window where items get added. 
        public void AddItem(Item item)
        {
            //inventoryManager.AddItem(item);
            Items.Add(item); // Sync with UI
            SaveData();
        }

        public void RemoveItem(int id)
        {
            inventoryManager.RemoveItem(id);
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null) Items.Remove(item); // Sync with UI
            SaveData();
        }

        public void UpdateItem(int id, Item updatedItem)
        {
            // Update the item in the InventoryManager
            inventoryManager.UpdateItem(id, updatedItem);

            // Update the item in the ObservableCollection (bound to the UI)
            var index = Items.IndexOf(Items.FirstOrDefault(i => i.Id == id));
            if (index >= 0)
            {
                Items[index] = updatedItem; // Replace the item directly
            }
            SaveData();
        }

        public void SaveData()
        {
            dataManager.SaveData(inventoryManager.GetItems());
        }
    }

}
