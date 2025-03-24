using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Data;

namespace InventoryManagement.UserManagement
{
    public class UserDataManager : GenericDataManager<User>
    {
        public UserDataManager() : base("UserData.json") { } // Call the base constructor with the file name
        private User _currentUser; 
        public User CurrentUser
        {
            get => _currentUser; 
            set
            {
                _currentUser = value; // Set the current user
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(isAdmin));
                OnPropertyChanged(nameof(isStaff));
            }
        }
        [JsonIgnore] // Ignore this property when serializing
        public bool isAdmin => CurrentUser?.Role == UserRole.Admin; // Check if the current user is an admin
        [JsonIgnore]
        public bool isStaff => CurrentUser?.Role == UserRole.Staff; // Check if the current user is staff
        protected override bool FilterItems(object obj) // Filter the users based on the search query
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return true;
            if (obj is User user)
            {
                return user.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       user.PasswordHash.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
        public static UserDataManager LoadData() // Load the user data
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "UserData.json"; // Get the file path

            if (!File.Exists(filePath)) // Check if the file exists
            {
                return new UserDataManager(); // If not: Return a new instance of UserDataManager
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented
            };

            var json = File.ReadAllText(filePath); // Read the file and store it in a string
            var data = JsonConvert.DeserializeObject<UserDataManager>(json, settings); // Deserialize the JSON string into a UserDataManager object

            data.ItemsView = CollectionViewSource.GetDefaultView(data.Items);
            data.ItemsView.Filter = data.FilterItems;
            data.ReattachEventHandlers();
            return data;
        }
        // Register a new user
        public static bool RegisterUser(string username, string password, UserRole selectedRole, ObservableCollection<User> users)
        {
            if (users.Any(user => user.Username == username)) // Check if the username already exists
            {
                return false;
            }

            users.Add(new User // Add the new user to the collection
            {
                Username = username,
                PasswordHash = ComputeHash(password, System.Security.Cryptography.SHA256.Create()),
                Role = selectedRole
            });
            return true;
        }
        private static string ComputeHash(string input, System.Security.Cryptography.SHA256 sha256) // Password hashing method
        {
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}