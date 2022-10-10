using CommunityToolkit.Mvvm.ComponentModel;
using MAUI_TASK_6.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TASK_6.Pages.Handheld
{
    [INotifyPropertyChanged]
    public partial class OrdersViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Order> _orders;

        public OrdersViewModel()
        {
            _orders = new ObservableCollection<Order>(AppData.Orders);
        }
    }
}
