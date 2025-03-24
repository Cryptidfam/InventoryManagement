using InventoryManagement.ItemManagement;
using InventoryManagement.UserManagement;
using System.Windows;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application // It's supposed to act like a singleton class
    {
        public App()
        {
            ItemData = ItemDataManager.LoadData(); 
            UserData = UserDataManager.LoadData();
        }
        public ItemDataManager ItemData { get;  set; } // Create a new instance of ItemDataManager
        public UserDataManager UserData { get; set; } // Create a new instance of UserDataManager
    }
}
