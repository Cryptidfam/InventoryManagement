namespace InventoryManagement.UserManagement
{
    public enum UserRole // Define the user roles
    {
        Admin, Staff 
    }
    public class User: BaseViewModel // User class
    {
        private string _username;
        public string Username
        {
            get => _username; set
            {
                if (_username != value)
                {
                    _username = value; // Set the username
                    OnPropertyChanged(nameof(Username)); // Notify the UI that the property has changed
                }
            }
        }
        private string _passwordHash;
        public string PasswordHash
        {
            get => _passwordHash; set
            {
                if (_passwordHash != value)
                {
                    _passwordHash = value;
                    OnPropertyChanged(nameof(PasswordHash));
                }
            }
        }
        
        private UserRole _role { get; set; }
        public UserRole Role { get => _role; set // Define the user role property
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged(nameof(Role));
                }
            } 
        }
    }
}
