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

namespace InventoryManagement.UserManagement
{
    /// <summary>
    /// Interaction logic for RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : Window
    {
        App app = (App)Application.Current;
        public RegisterUserControl()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Register user. Jump to 'LoadUserData'
            bool success = UserDataManager.RegisterUser(username, password, app.data.Users); 
            if (success)
            {
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Close the registration window
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
