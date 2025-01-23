using InventoryManagement.Management;
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
        public App()
        {
            // Optionally, force loading the data on startup
            //var items = DataManager.Items;
            data = DataManager.LoadData();

        }



        public DataManager data { get;  set; }



    }
}
