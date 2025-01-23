using InventoryManagement.Management;
using System.Linq;
using System.Windows;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window
    {
        private readonly Item _item;
        App app = (App)Application.Current;
        public AddItems(Item item)
        {
            InitializeComponent();
            _item = item; // Assign the item
            DataContext = _item; // Bind to DataContext
        }


        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (app.data.Items == null)
            {
                MessageBox.Show("DataManager.Items is not initialized. Cannot add the item.");
                return;
            }

            if (_item == null)
            {
                MessageBox.Show("_item is null. Cannot add the item.");
                return;
            }

            // 'Object reference not set to an instance of an object.' at existingItem.Id == _item.Id:
            // if (!DataManager.Items.Any(existingItem => existingItem.Id == _item.Id)) 
            if (!app.data.Items.Any(existingItem => existingItem != null && existingItem.Id == _item.Id)) // THIS WORKED, HUH???
            {
                app.data.Items.Add(_item);
            }

            Close(); // Close the dialog
        }


    }


}
