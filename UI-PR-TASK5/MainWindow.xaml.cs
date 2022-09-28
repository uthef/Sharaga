using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_PR_TASK5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindToggleButtons();
        }

        private void BindToggleButtons()
        {
            foreach (ListViewItem item in TimerControl.TimeList.Items)
            {
                var grid = item.Content as Grid;
                var stackPanel = grid.Children[0] as StackPanel;

                IEnumerable<ToggleButton> toggleButtons =
                        from object element in grid.Children
                        where element is ToggleButton
                        select element as ToggleButton;

                IEnumerable<TextBlock> textBlocks =
                    from object element in stackPanel.Children
                    where element is TextBlock
                    select element as TextBlock;

                var toggleButton = toggleButtons.First();
                toggleButton.Click += (s, args) => OnToggleButtonClicked(toggleButton, textBlocks.First(), textBlocks.Last());
            }
        }

        private Timer BindTimer(TextBlock textBlock, ToggleButton button)
        {
            string initialText = textBlock.Text;
            string[] timeTextSplit = initialText.Split(':');
            DateTime startDate = DateTime.Now;

            TimeSpan time = TimeSpan.FromMinutes(int.Parse(timeTextSplit[0]));
            time.Add(TimeSpan.FromSeconds(int.Parse(timeTextSplit[1])));

            DateTime finishDate = startDate + time;

            Timer timer = new Timer(1);
            timer.Elapsed += (s, args) =>
            {
                try
                {
                    var diff = (time - (DateTime.Now - startDate));
                    Dispatcher.Invoke(() => textBlock.Text = diff.ToString("mm':'ss':'ff"));

                    if (DateTime.Now >= finishDate) timer.Dispose();
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Timer has been canceled");
                }
            };

            timer.Disposed += (s, args) =>
            {
                Dispatcher.Invoke(() =>
                {
                    textBlock.Text = initialText;
                    button.IsChecked = false;
                });
            };

            textBlock.Tag = timer;
            timer.Start();

            return timer;
        }

        private void OnToggleButtonClicked(ToggleButton toggleButton, TextBlock timeText, TextBlock stateText)
        {
            if (toggleButton.IsChecked is true)
            {
                stateText.Text = "Вкл.";
                BindTimer(timeText, toggleButton);
            }
            else
            {
                if (timeText.Tag is Timer timer)
                {
                    timer.Dispose();
                    timeText.Tag = null;
                }

                stateText.Text = "Выкл.";
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
