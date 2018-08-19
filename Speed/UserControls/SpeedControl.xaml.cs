namespace Speed.UserControls
{
    public partial class SpeedControl
    {
        private ISimulatorSpeed _simulatorSpeed;

        public ISimulatorSpeed SimulatorSpeed
        {
            get => _simulatorSpeed;
            set
            {
                if (_simulatorSpeed != null)
                    _simulatorSpeed.SpeedChanged -= OnSpeedChanged;

                _simulatorSpeed = value;

                if (_simulatorSpeed != null)
                    _simulatorSpeed.SpeedChanged += OnSpeedChanged;
            }
        }

        private void OnSpeedChanged(double speed)
        {
            if (Dispatcher.CheckAccess())
            {
                _tbSpeedMetersPerSecond.Text = speed.ToString("F1") + " м/с";
                _tbSpeedKilometersPerHour.Text = (speed * 60 * 60 / 1000).ToString("F1") + " км/ч";
            }
            else
                Dispatcher.Invoke(() => OnSpeedChanged(speed));
        }

        public SpeedControl()
        {
            InitializeComponent();
        }
    }
}
