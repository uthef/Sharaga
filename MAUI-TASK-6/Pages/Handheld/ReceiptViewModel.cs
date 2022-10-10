using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TASK_6.Models;

namespace MAUI_TASK_6.Pages.Handheld;

[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]
public partial class ReceiptViewModel
{
    [ObservableProperty]
    Order order;

    [RelayCommand]
    async void Done()
    {
        await Shell.Current.GoToAsync("///orders");
    }
}

