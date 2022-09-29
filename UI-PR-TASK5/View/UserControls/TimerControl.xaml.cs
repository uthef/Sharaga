using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_PR_TASK5.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TimerControl.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        public TimerControl()
        {
            InitializeComponent();
        }

        public void AddTimers(params TimeSpan[] items)
        {
            foreach (var item in items)
                TimerList.Items.Add(new TimerListItem(item));
        }

        public void DisposeAllTimers()
        {
            foreach (TimerListItem item in TimerList.Items)
            {
                item.DisposeTimer();
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
