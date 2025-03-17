using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Windows.Data;

namespace InventoryManagement.UserManagement
{    
    public class UserDataManager : BaseViewModel
    {
        public UserDataManager()
        {
            Users = new ObservableCollection<User>();
            UsersView = CollectionViewSource.GetDefaultView(Users);
            UsersView.Filter = FilterUsers;
        }

        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "UserData.json";

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if (_users != value)
                {
                    if (_users != null)
                    {
                        _users.CollectionChanged -= Users_CollectionChanged;
                        foreach (var user in _users)
                        {
                            user.PropertyChanged -= Users_PropertyChanged;
                        }
                    }

                    _users = value ?? new ObservableCollection<User>();

                    if (_users != null)
                    {
                        _users.CollectionChanged += Users_CollectionChanged;
                        foreach (var user in _users)
                        {
                            user.PropertyChanged += Users_PropertyChanged;
                        }
                    }
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(isAdmin));
                OnPropertyChanged(nameof(isStaff));
            }
        }

        [JsonIgnore]
        public bool isAdmin => CurrentUser?.Role == UserRole.Admin;
        [JsonIgnore]
        public bool isStaff => CurrentUser?.Role == UserRole.Staff;

        private string _searchUserQuery;
        public string SearchUserQuery
        {
            get => _searchUserQuery;
            set
            {
                _searchUserQuery = value;
                OnPropertyChanged(nameof(SearchUserQuery));
                UsersView.Refresh();
            }
        }

        private ICollectionView _usersView;
        [JsonIgnore]
        public ICollectionView UsersView
        {
            get => _usersView;
            set
            {
                _usersView = value;
                OnPropertyChanged(nameof(UsersView));
            }
        }

        public static UserDataManager LoadData()
        {
            if (!File.Exists(filePath))
            {
                return new UserDataManager();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<UserDataManager>(json, settings);

            data.UsersView = CollectionViewSource.GetDefaultView(data.Users);
            data.UsersView.Filter = data.FilterUsers;
            data.ReattachEventHandlers();
            return data;
        }

        private bool FilterUsers(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchUserQuery)) return true;
            if (obj is User user)
            {
                return user.Username.Contains(SearchUserQuery, StringComparison.OrdinalIgnoreCase) ||
                       user.PasswordHash.Contains(SearchUserQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void ReattachEventHandlers()
        {
            if (Users != null)
            {
                Users.CollectionChanged += Users_CollectionChanged;
                foreach (var user in Users)
                {
                    user.PropertyChanged += Users_PropertyChanged;
                }
            }
        }

        private void Users_PropertyChanged(object sender, PropertyChangedEventArgs e) => SaveData();
        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => SaveData();

        public void SaveData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(filePath, json);
        }

        public static bool RegisterUser(string username, string password, UserRole selectedRole, ObservableCollection<User> Users)
        {
            if (Users.Any(user => user.Username == username))
            {
                return false;
            }

            Users.Add(new User
            {
                Username = username,
                PasswordHash = ComputeHash(password, System.Security.Cryptography.SHA256.Create()),
                Role = selectedRole
            });
            return true;
        }

        private static string ComputeHash(string input, System.Security.Cryptography.SHA256 sha256)
        {
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}