using System;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace InventoryManagement.UserManagement
{
    public class LoginViewModel : BaseViewModel
    {
        readonly App app = (App)Application.Current; // Get the current application instance

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value; // Set the username
                    OnPropertyChanged(); // Notify the UI that the property has changed
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged(); // Enable the login button if the username is not empty
                }
            }
        }

        private string _password;
        public string Password
        {
            private get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value; 
                    OnPropertyChanged();
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; } 
        public LoginViewModel() // Constructor, initialize the commands
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin); 
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
        }
        private bool CanExecuteLogin(object parameter) =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password); // Check if the username and password are not empty
        private void ExecuteLogin(object parameter)
        {
            var user = ((App)Application.Current).UserData.Items.FirstOrDefault(u => u.Username == Username); // Find the user by username

            if (user != null && VerifyPassword(Password, user.PasswordHash)) // Verify the password
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                app.UserData.CurrentUser = user; // Set the current user
                OpenMainWindow();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static void OpenMainWindow() 
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
        private void OpenRegisterWindow(object parameter)
        {
            var registerWindow = new RegisterUserControl();
            registerWindow.Show();
        }
        private static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return ComputeHash(enteredPassword) == storedHash; // Compare the entered password with the stored hash
        }
        private static string ComputeHash(string input) // Compute the hash of the input string
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
