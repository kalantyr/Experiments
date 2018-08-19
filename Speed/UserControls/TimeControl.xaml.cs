using System.Windows;

namespace Speed.UserControls
{
    public partial class TimeControl
    {
        private ISimulatorTimer _simulatorTimer;

        public TimeControl()
        {
            InitializeComponent();
        }

        public ISimulatorTimer SimulatorTimer
        {
            get => _simulatorTimer;
            set
            {
                if (_simulatorTimer != null)
                    _simulatorTimer.ElapsedTimeChanged -= _simulatorTimer_ElapsedTimeChanged;

                _simulatorTimer = value;

                if (_simulatorTimer != null)
                    _simulatorTimer.ElapsedTimeChanged += _simulatorTimer_ElapsedTimeChanged;
            }
        }

        private void _simulatorTimer_ElapsedTimeChanged(System.TimeSpan elapsedTime)
        {
            if (Dispatcher.CheckAccess())
                _tbElapsedTime.Text = elapsedTime.ToString(@"hh\:mm\:ss");
            else
                Dispatcher.Invoke(() => _simulatorTimer_ElapsedTimeChanged(elapsedTime));
        }

        private void OnStartClick(object sender, RoutedEventArgs e)
        {
            SimulatorTimer.Start();
        }
    }
}
