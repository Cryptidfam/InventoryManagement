using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Item: BaseViewModel
    {
        private int _id;
        public int Id
        {
            get => _id; set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private string _name;
        public string Name
        {
            get => _name; set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _category;
        public string Category
        {
            get => _category; set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }
        private int _quantity;
        public int Quantity
        {
            get => _quantity; set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        private decimal _price;
        public decimal Price
        {
            get => _price; set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }
        }

        // public string Description { get; set; } // I'm not sure if I'll be keeping this one
    }

}
