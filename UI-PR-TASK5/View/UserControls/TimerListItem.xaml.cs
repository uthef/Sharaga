using System;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace UI_PR_TASK5.View.UserControls
{
    public partial class TimerListItem : UserControl, INotifyPropertyChanged
    {
        string _timeText;
        bool _state;
        DateTime _startTime, _finishTime;
        TimeSpan _time;
        CancellationTokenSource _dispatcherTokenSource;
        private Timer _timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeText 
        { 
            get => _timeText;
            private set
            {
                _timeText = value;
                NotifyPropertyChanged(nameof(TimeText));
            }
        }

        public TimeSpan Time
        {
            get => _time;
            set => TimeText = (_time = value).ToString("mm':'ss':'ff");
        }

        public readonly TimeSpan InitialTime;

        public string StateText { get; private set; }

        public bool IsActive { get => _timer != null; }

        public bool State
        {
            get => _state;
            set
            {
                StateText = (_state = value) ? "Вкл." : "Выкл.";
                NotifyPropertyChanged(nameof(StateText));
            }
        }

        public TimerListItem(TimeSpan initialTime)
        {
            Time = InitialTime = initialTime;
            State = false;

            InitializeComponent();
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void ToggleButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsActive)
            {
                DisposeTimer();
                return;
            }
            
            _dispatcherTokenSource = new CancellationTokenSource();
            _timer = new Timer(TimerTick, null, 0, 10);
;
            State = true;

            _startTime = DateTime.Now;
            _finishTime = _startTime + InitialTime;
        }

        public void DisposeTimer()
        {
            _dispatcherTokenSource.Cancel();
            _timer?.Dispose();
            _timer = null;

            ToggleBt.IsChecked = false;
            State = false;
            Time = InitialTime;
        }

        private void TimerTick(object state)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    Time = InitialTime - (DateTime.Now - _startTime);
                    if (DateTime.Now >= _finishTime) DisposeTimer();
                },
                DispatcherPriority.Render, _dispatcherTokenSource.Token);
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine($"TimerListItem\n\tDispatcher: {e.Message}");
            }
        }
    }
}