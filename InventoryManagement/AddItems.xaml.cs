using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly MainViewModel viewModel;

        private Item _items;
        public Item Items
        {
            get => _items; set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }
        public AddItems(MainViewModel viewmodel = null)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
            viewModel = viewmodel;
        }
        // To do: Add texboxes in AddItems.xaml instead of DataGridTextColumn.
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Example of adding a new item(replace with your input mechanism!!!)
             viewModel.AddItem(viewModel.SelectedItem);
            Close();
           // viewModel.Items.Add(Items);
        }

        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            // This will update an item when something gets changed... Later.
            // You should probably add a pop up window asking if changes should be made.
        }

    }
}
