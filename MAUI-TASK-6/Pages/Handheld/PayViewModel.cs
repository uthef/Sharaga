using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TASK_6.Models;

namespace MAUI_TASK_6.Pages.Handheld
{
    [INotifyPropertyChanged]
    [QueryProperty("Order", "Order")]
    public partial class PayViewModel
    {
        [ObservableProperty]
        Order order;

        [RelayCommand]
        async void Pay()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"Order", order }
            };

            await Shell.Current.GoToAsync($"{nameof(SignaturePage)}", 
                navigationParameter);
        }
    }
}
