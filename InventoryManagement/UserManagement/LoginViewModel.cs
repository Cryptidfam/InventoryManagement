using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

// https://www.youtube.com/watch?v=Fs2gwb6Dqjk
namespace InventoryManagement.UserManagement
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; } // Remove later. Remember User.cs is for getters and setters.
        public string Password { get; set; } // Same thing here.

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            Username = "test"; // Dummy value (So I can 'login')
            Password = "test"; // Dummy value
            // Initialize the command
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
        }

        private bool CanExecuteLogin(object parameter)
        {
            // Enable the button only if the username and password are filled
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            // To do: Add login logic (e.g., validate against a database)
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        private void OpenRegisterWindow(object parameter)
        {
            // Open the registration window
            var registerWindow = new RegisterUserControl();
            registerWindow.Show();
        }
    }
}
