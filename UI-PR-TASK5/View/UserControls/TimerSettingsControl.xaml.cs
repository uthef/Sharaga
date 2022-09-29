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
    /// Логика взаимодействия для TimerSettingsControl.xaml
    /// </summary>
    public partial class TimerSettingsControl : UserControl
    {
        public DialogHost DialogHost;
        public TimerSettingsControl(DialogHost dialogHost)
        {
            InitializeComponent();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
