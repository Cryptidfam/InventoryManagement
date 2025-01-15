using InventoryManagement.Management;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyCollectionChanged
    {
        public DataManager dataManager { get; set; }
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items.CollectionChanged -= Items_CollectionChanged;
                    _items = value;
                    _items.CollectionChanged += Items_CollectionChanged;
                }
            }
        }
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            dataManager.SaveData();
        }
        // public Item SelectedItem { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public MainWindow()
        {
            InitializeComponent();
            // dataManager = DataManager.LoadData();
            // Load items from JSON and set the DataContext
            DataContext = this;
        }

        // Currently I am trying to make it so the items actually gets saved.
        private void AddItemWindow_Click(object sender, RoutedEventArgs e)
        {
            // Pass a new item to the AddItems window
            var newItem = new Item();
            var addItemWindow = new AddItems(newItem);
            addItemWindow.ShowDialog();

            // 'Object reference not set to an instance of an object.'
            if (!Items.Contains(newItem))
            {
                Items.Add(newItem); // Add to collection
                SaveItemsToJson();  // Save to JSON
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryGrid.SelectedItem is Item selectedItem)
            {
                Items.Remove(selectedItem);
                SaveItemsToJson(); // Save the updated list
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        // No overload for method 'SaveData' takes 1 arguments
        private void SaveItemsToJson()
        {
            dataManager.SaveData(Items.ToList()); // Save to JSON
        }
    }

}
