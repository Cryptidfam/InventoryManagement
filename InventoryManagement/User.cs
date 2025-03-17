using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public enum UserRole 
    {
        Admin, Staff
    }
    public class User: BaseViewModel
    {
        private string _username;
        public string Username
        {
            get => _username; set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
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
        public UserRole Role { get => _role; set
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
