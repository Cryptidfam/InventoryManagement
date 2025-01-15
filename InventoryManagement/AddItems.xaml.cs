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

    // This should be simplified 
    public partial class AddItems : Window
    {
        public AddItems(Item item)
        {
            InitializeComponent();
            DataContext = item;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Simply close the window; the MainWindow handles adding the item
        }
    }

}
