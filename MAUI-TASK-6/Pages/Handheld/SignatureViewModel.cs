using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI_TASK_6.Messages;
using MAUI_TASK_6.Models;

namespace MAUI_TASK_6.Pages.Handheld;

[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]
public partial class SignatureViewModel
{
    [ObservableProperty]
    Order order;

    [RelayCommand]
    async Task Done()
    {
        WeakReferenceMessenger.Default.Send(
            new SaveSignatureMessage(order.Table)
        );

        var navigationParameter = new Dictionary<string, object>
        {
            {"Order", order }
        };
        await Shell.Current.GoToAsync($"{nameof(ReceiptPage)}", navigationParameter);
    }

    [RelayCommand]
    void Clear()
    {
        WeakReferenceMessenger.Default.Send(
            new ClearSignatureMessage(true)
        );
    }
}


