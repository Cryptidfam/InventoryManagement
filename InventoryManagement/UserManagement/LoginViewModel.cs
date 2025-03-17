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
        readonly App app = (App)Application.Current;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
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
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
        }

        private bool CanExecuteLogin(object parameter) =>
            !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        private void ExecuteLogin(object parameter)
        {
            var user = ((App)Application.Current).UserData.Users.FirstOrDefault(u => u.Username == Username);

            if (user != null && VerifyPassword(Password, user.PasswordHash))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                app.UserData.CurrentUser = user;
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
            return ComputeHash(enteredPassword) == storedHash;
        }
        private static string ComputeHash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
