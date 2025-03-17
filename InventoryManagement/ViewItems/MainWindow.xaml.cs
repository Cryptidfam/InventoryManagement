using InventoryManagement.UserManagement;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly App app = (App)Application.Current;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = app; // Set the data context to the App instance
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (app.ItemData.Items == null)
            {
                MessageBox.Show("DataManager.Items is not initialized. Cannot add the item.");
                return;
            }
            string name = NameTextBox.Text;
            string category = CategoryTextBox.Text;
            bool quantityParsed = int.TryParse(QuantityTextBox.Text, out int quantity);
            bool priceParsed = decimal.TryParse(PriceTextBox.Text, out decimal price);
            bool idParsed = int.TryParse(IdTextBox.Text, out int id);

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category) || !idParsed || id <= 0)
            {
                MessageBox.Show("Required fields: Name, Category, and ID (must be a positive number).", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (app.ItemData.Items.Any(existingItem => existingItem != null && existingItem.Id == id))
            {
                MessageBox.Show($"An item with ID {id} already exists.", "Duplicate ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newItem = new Item
            {
                Name = name,
                Category = category,
                Quantity = quantityParsed ? quantity : 0,
                Price = priceParsed ? price : 0,
                Id = id
            };

            app.ItemData.Items.Add(newItem);
            MessageBox.Show("Item added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            NameTextBox.Clear();
            CategoryTextBox.Clear();
            QuantityTextBox.Clear();
            PriceTextBox.Clear();
            IdTextBox.Clear();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryGrid.SelectedItem is Item selectedItem)
            {
                app.ItemData.Items.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(); 
            loginWindow.Show();
            this.Close();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {        
            if (SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortOption = selectedItem.Tag.ToString();

                switch (sortOption)
                {
                    case "IdAsc":
                        app.ItemData.SortBy(nameof(Item.Id), ListSortDirection.Ascending);
                        break;
                    case "IdDesc":
                        app.ItemData.SortBy(nameof(Item.Id), ListSortDirection.Descending);
                        break;
                    case "NameAsc":
                        app.ItemData.SortBy(nameof(Item.Name), ListSortDirection.Ascending);
                        break;
                    case "NameDesc":
                        app.ItemData.SortBy(nameof(Item.Name), ListSortDirection.Descending);
                        break;
                    case "PriceAsc":
                        app.ItemData.SortBy(nameof(Item.Price), ListSortDirection.Ascending);
                        break;
                    case "PriceDesc":
                        app.ItemData.SortBy(nameof(Item.Price), ListSortDirection.Descending);
                        break;
                }
            }
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManage userManageWindow = new UserManage();
            userManageWindow.Show();
        }
    }



}
