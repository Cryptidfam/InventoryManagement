using InventoryManagement.Management;
using System.Collections.ObjectModel;
using System.Windows;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App app = (App)Application.Current;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = app.data.Items; // Bind the Items collection. Something something anonymous object.

            // DataContext = Application.Current as App; // Or directly bind to DataManager.Items
        }

        private void AddItemWindow_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new Item();
            var addItemWindow = new AddItems(newItem);
            addItemWindow.ShowDialog();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryGrid.SelectedItem is Item selectedItem)
            {
                app.data.Items.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }
    }


}
