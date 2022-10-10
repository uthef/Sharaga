using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUI_TASK_6.Models;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace MAUI_TASK_6.Pages.Handheld
{
    [INotifyPropertyChanged]
    [QueryProperty("Order", "Order")]
    [QueryProperty("Added", "Added")]
    public partial class OrderDetailsViewModel
    {
        [ObservableProperty]
        Order order;

        [ObservableProperty]
        Item added;

        [RelayCommand]
        async Task Pay()
        {
            try
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    {"Order", order }
                };
                await Shell.Current.GoToAsync($"{nameof(TipPage)}", navigationParameter);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        async Task Add()
        {
            await Shell.Current.GoToAsync($"{nameof(ScanPage)}");
        }
    }

}

