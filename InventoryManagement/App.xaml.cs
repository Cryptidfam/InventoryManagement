using InventoryManagement.Management;
using InventoryManagement.UserManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // private readonly DataManager dataManager;
        public App()
        {
            ItemData = DataManager.LoadData();
            UserData = UserDataManager.LoadData();
        }
        public DataManager ItemData { get;  set; }
        public UserDataManager UserData { get; set; }
    }
}
