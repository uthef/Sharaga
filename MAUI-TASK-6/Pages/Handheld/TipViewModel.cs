using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TASK_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_TASK_6.Pages.Handheld;
[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]

public partial class TipViewModel
{
    [ObservableProperty]
    Order order;

    [ObservableProperty]
    double tip;

    partial void OnTipChanged(double value)
    {
        order.Tip = value;
        OnPropertyChanged(nameof(Order));
    }

    [RelayCommand]
    async void Continue()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"Order", order }
        };
        await Shell.Current.GoToAsync($"{nameof(PayPage)}", navigationParameter);
    }
}

