using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Markup;
using System.Runtime.CompilerServices;

namespace InventoryManagement.Management
{
    public class DataManager: INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "AppData.json";

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items == value) return;

                if (_items != null)
                {
                    _items.CollectionChanged -= Items_CollectionChanged;

                    foreach (var item in _items)
                    {
                        item.PropertyChanged -= Items_PropertyChanged;
                    }


                    _items = value;

                    _items.CollectionChanged += Items_CollectionChanged;

                    foreach (var item in _items)
                    {
                        item.PropertyChanged += Items_PropertyChanged;
                    }
                }
            }
        }

        private ObservableCollection<User> _users = new ObservableCollection<User>();



        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if (_users == value) return;

                if (_users != null)
                {
                    _users.CollectionChanged -= Users_CollectionChanged;

                    foreach (var user in _users)
                    {
                        user.PropertyChanged -= Users_PropertyChanged;
                    }


                    _users = value;

                    _users.CollectionChanged += Users_CollectionChanged;

                    foreach (var user in _users)
                    {
                        user.PropertyChanged += Users_PropertyChanged;
                    }
                }
            }
        }

        private void Users_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveData();
        }
        private void Items_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveData();

        }

        private void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SaveData();
        }
        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveData(); // Automatically save whenever items change
        }




        public static DataManager LoadData()
        {
            if (!File.Exists(filePath))
            {
                return new DataManager();
            }
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<DataManager>(json,settings);
        }

        public void SaveData()
        {
            JsonSerializerSettings settings =new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(filePath, json);
        }
    }
}
