using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TASK_6.Models
{
    [INotifyPropertyChanged]
    public partial class Item
    {
        [ObservableProperty]
        string title;

        [ObservableProperty]
        int quantity;

        [ObservableProperty]
        string image;

        [ObservableProperty]
        double price;

        public void OnQuantityChanged(int value)
        {
            OnPropertyChanged(nameof(SubTotal));
        }

        public ItemCategory Category { get; set; }

        public double SubTotal
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
