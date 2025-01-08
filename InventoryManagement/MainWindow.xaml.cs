using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewmodel;

        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new MainViewModel();
            DataContext = viewmodel;
        }
        private void AddItemWindow_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the AddItems window, passing the shared ViewModel
            var addItemWindow = new AddItems(viewmodel);
            addItemWindow.ShowDialog(); // Opens the window modally
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // Example of removing the selected item
            if (InventoryGrid.SelectedItem is Item selectedItem)
            {
                viewmodel.RemoveItem(selectedItem.Id);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }


    }
}
