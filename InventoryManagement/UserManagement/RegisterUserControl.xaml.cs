using System.Windows;
using System.Windows.Controls;

namespace InventoryManagement.UserManagement
{
    /// <summary>
    /// Interaction logic for RegisterUserControl.xaml
    /// </summary>
    public partial class RegisterUserControl : Window 
    {
        readonly App app = (App)Application.Current; // Get the current application instance
        public RegisterUserControl()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from the input fields
            string username = UsernameBox.Text; 
            string password = PasswordBox.Password;
            string passwordConfirm = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) // Check if the username and password are empty
            {
                MessageBox.Show("Username and password cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != passwordConfirm) // Check if the passwords match
            {
                MessageBox.Show("Passwords don't match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserRole selectedRole = UserRole.Staff; // Default to Staff
            if (RoleComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag.ToString() == "Admin")
            {
                selectedRole = UserRole.Admin; // Set the role to Admin if selected
            }

            bool success = UserDataManager.RegisterUser(username, password, selectedRole, app.UserData.Items); // Register the user
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
