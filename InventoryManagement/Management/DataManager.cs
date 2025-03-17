using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Data;

namespace InventoryManagement.Management
{
    public class DataManager : BaseViewModel, INotifyCollectionChanged
    {
        #region PropertyChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        public DataManager()
        {
            Items = new ObservableCollection<Item>();
            ItemsView = CollectionViewSource.GetDefaultView(Items);
            ItemsView.Filter = FilterItems;
        }

        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "AppData.json";

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    if (_items != null)
                    {
                        _items.CollectionChanged -= Items_CollectionChanged;
                        foreach (var item in _items)
                        {
                            item.PropertyChanged -= Items_PropertyChanged;
                        }
                    }
                    _items = value ?? new ObservableCollection<Item>();
                    if (_items != null)
                    {
                        _items.CollectionChanged += Items_CollectionChanged;
                        foreach (var item in _items)
                        {
                            item.PropertyChanged += Items_PropertyChanged;
                        }
                    }
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        private ObservableCollection<Item> _filteredItems;
        public ObservableCollection<Item> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged(nameof(FilteredItems));
            }
        }

        private ICollectionView _itemsView;
        [JsonIgnore]
        public ICollectionView ItemsView
        {
            get => _itemsView;
            set
            {
                _itemsView = value;
                OnPropertyChanged(nameof(ItemsView));
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                ItemsView.Refresh(); // Refresh the view to apply the filter
            }
        }

        private bool FilterItems(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return true;
            if (obj is Item item)
            {
                return item.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                       item.Category.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public void SortBy(string propertyName, ListSortDirection direction)
        {
            ItemsView.SortDescriptions.Clear(); // Clear existing sort descriptions
            ItemsView.SortDescriptions.Add(new SortDescription(propertyName, direction));
        }

        private void ReattachEventHandlers()
        {
            if (Items != null)
            {
                Items.CollectionChanged += Items_CollectionChanged;
                foreach (var item in Items)
                {
                    item.PropertyChanged += Items_PropertyChanged;
                }
            }
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
            var data = JsonConvert.DeserializeObject<DataManager>(json, settings);

            data.ItemsView = CollectionViewSource.GetDefaultView(data.Items);
            data.ItemsView.Filter = data.FilterItems;
            data.ReattachEventHandlers();
            return data;
        }

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

        private void Items_PropertyChanged(object sender, PropertyChangedEventArgs e) => SaveData();
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => SaveData();
    }
}
