using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Collections.ObjectModel;
namespace InventoryManagement
{
    public abstract class GenericDataManager<T> : BaseViewModel where T : INotifyPropertyChanged // Generic class for managing data
    {
        protected GenericDataManager(string fileName)
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + fileName; // Set the file path
            Items = new ObservableCollection<T>(); // Initialize the Items collection
            ItemsView = CollectionViewSource.GetDefaultView(Items); // Get the default view of the Items collection
            ItemsView.Filter = FilterItems; // Set the filter for the Items view
        }
        protected string FilePath { get; } // Get the file path

        private ObservableCollection<T> _items; // Collection of items
        public ObservableCollection<T> Items // Property for the collection of items
        {
            get => _items;
            set
            {
                if (_items != value) // Check if the value has changed
                {
                    DetachEventHandlers();
                    _items = value ?? new ObservableCollection<T>(); // If value is null, create a new ObservableCollection<T>()
                    AttachEventHandlers();
                    OnPropertyChanged(nameof(Items)); // Notify the UI that the property has changed
                }
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
        public string SearchQuery // Filter the items based on the search query
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value; 
                OnPropertyChanged(nameof(SearchQuery));
                ItemsView.Refresh(); // Refresh the view
            }
        }
        protected void AttachEventHandlers() // Attach event handlers to the Items collection and each item in the collection
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
        protected void DetachEventHandlers() // Detach event handlers from the Items collection and each item in the collection
        {
            if (Items != null) // Check if the Items collection is initialized
            {
                Items.CollectionChanged -= Items_CollectionChanged; // Remove the event handler
                foreach (var item in Items) // Loop through each item in the collection
                {
                    item.PropertyChanged -= Items_PropertyChanged; // Remove the event handler
                }
            }
        }
        protected virtual bool FilterItems(object obj) => true;
        public void SortBy(string propertyName, ListSortDirection direction) // Sort the items by a property
        {
            ItemsView.SortDescriptions.Clear(); // Clear the existing sort descriptions
            ItemsView.SortDescriptions.Add(new SortDescription(propertyName, direction)); // Add the new sort description
        }
        protected virtual void ReattachEventHandlers() => AttachEventHandlers(); // Reattach event handlers after deserialization

        // This is not used in the current implementation
        public static TManager LoadData<TManager>(string filePath) where TManager : GenericDataManager<INotifyPropertyChanged>, new()
        {
            if (!File.Exists(filePath))
            {
                return new TManager();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings() // Could be a seperate method
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented
            };

            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<TManager>(json, settings);

            data.ItemsView = CollectionViewSource.GetDefaultView(data.Items);
            data.ItemsView.Filter = data.FilterItems;
            data.ReattachEventHandlers();
            return data;
        }
        public void SaveData() // Save the data to a file
        {
            JsonSerializerSettings settings = new JsonSerializerSettings 
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(FilePath, json);
        }
        private void Items_PropertyChanged(object sender, PropertyChangedEventArgs e) => SaveData();
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => SaveData();
    }
}
